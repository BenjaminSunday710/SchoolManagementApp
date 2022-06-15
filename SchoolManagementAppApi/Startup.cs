using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolManagementApp.Infrastructure.Mappings;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using SchoolManagementAppApi.ApplicationService.MiddleWares;
using Shared.Application.Mediator;
using Shared.Application.Mediators;
using Shared.Infrastructure;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Mappings;
using UserManagement.Infrastructure.Security.TokenService;

namespace SchoolManagementAppApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var environment = env.IsDevelopment() ? "Development" : "Production";
            Configuration = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.{environment}.json")
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetValue<string>("DefaultConnection");

            services.AddHttpContextAccessor();

            var userMgtMapAssembly = Assembly.GetAssembly(typeof(UserMap));
            var coreModuleMapAssembly = Assembly.GetAssembly(typeof(SchoolMap));

            var mappingAssemblies = new List<Assembly>();
            mappingAssemblies.Add(userMgtMapAssembly);
            mappingAssemblies.Add(coreModuleMapAssembly);
            services.AddScoped<INHibernateHelper>(x => new NHibernateHelper(connectionString, mappingAssemblies));

            services.AddUserManagementModule();

            services.AddMigrationService(connectionString);

            services.AddScoped<IMediator, Mediator>();

            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "School Management App API",
                    Version = "V1"
                });
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer Scheme. \r\n\r\n 'Bearer' [space] and then your token in the text input below." +
                    "\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference=new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{ }
                    }
                });
            });

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            jwtSettings = new JwtSettings();
            Configuration.GetSection("JwtSettings").Bind(jwtSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });

            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddAuthorization();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.RunMigration();

            await app.SeedPermissions(Configuration);

            await app.SeedAdmin(Configuration);

            await app.SeedDefaultRoles();
        }

        private JwtSettings jwtSettings;
    }
}
