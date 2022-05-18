using Microsoft.Extensions.DependencyInjection;
using SchoolManagementApp.Infrastructure.Mappings;
using Shared.Infrastructure;
using System.Reflection;

namespace SchoolManagementApp.Infrastructure
{
    public static class CoreModule
    {
        public static IServiceCollection AddCoreModule(this IServiceCollection services,string connectionString)
        {
            var assembly = Assembly.GetAssembly(typeof(SchoolMap));
            services.AddScoped<INHibernateHelper>(x => new NHibernateHelper(connectionString, assembly));

            return services;
        }
    }
}
