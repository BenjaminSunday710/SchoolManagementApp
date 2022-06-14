using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Security.TokenService;

namespace SchoolManagementAppApi.ApplicationService.MiddleWares
{
    public class JwtMiddleWare
    {
        public JwtMiddleWare(RequestDelegate next, IConfiguration configuration, IUserIdentity user)
        {
            _configuration = configuration;
            _next = next;
            _user = user;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null) AttachAccountToContext(context, token);

            await _next(context);
        }

        private void AttachAccountToContext(HttpContext context, string token)
        {
            var jwtSettings = new JwtSettings();
            _configuration.GetSection("JwtSettings").Bind(jwtSettings);
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.Sid).Value;
                context.Items["User"] = _user;//userService that goes to the db and fetch the user
                //then, there's no need for UserIdentity
            }
            catch (Exception)
            {
                //do nothing if jwt validation fails
                //account is not attached to context so request womt have access to secure routes
            }
        }

        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IUserIdentity _user;
    }
}
