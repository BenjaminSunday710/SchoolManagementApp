using Microsoft.Extensions.DependencyInjection;
using Shared.Infrastructure;
using System.Reflection;
using UserManagement.Infrastructure.Mappings;

namespace UserManagement.Infrastructure
{
    public static class UserManagementModule
    {
        public static IServiceCollection AddUserManagementModule(this IServiceCollection services, string connectionString)
        {
            var assembly = Assembly.GetAssembly(typeof(UserMap));
            services.AddScoped<INHibernateHelper>(x => new NHibernateHelper(connectionString, assembly));

            return services;
        }
    }
}
