using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using SchoolManagementApp.Infrastructure.Mappings;
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

            //services.AddCoreModule();

            services.AddMigrationService(connectionString);

            services.AddScoped<IMediator, Mediator>();

            services.Configure<JwtSettings>(Configuration.GetSection("JwtSettings"));

            var jwtSettings = new JwtSettings();
            Configuration.GetSection("JwtSettings").Bind(jwtSettings);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = jwtSettings.Issuer,
                        ValidAudience = jwtSettings.Audience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                    };
                });


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "School Management App API",
                    Version = "V1"
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}
