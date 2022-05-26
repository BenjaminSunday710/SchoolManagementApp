using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure;
using System.Reflection;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Mappings;
using UserManagement.Infrastructure.Security;
using UserManagement.Infrastructure.Security.TokenService;

namespace UserManagement.Infrastructure
{
    public static class UserManagementModule
    {
        public static IServiceCollection AddUserManagementModule(this IServiceCollection services)
        {
            services.AddScoped<ITokenProvider, TokenProvider>();
            services.AddScoped<IUserIdentity, UserIdentity>();

            return services;
        }
    }
}
