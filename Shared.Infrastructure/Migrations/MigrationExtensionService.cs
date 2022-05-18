using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Shared.Infrastructure
{
    public static class MigrationExtensionService
    {
        public static IApplicationBuilder RunMigration(this IApplicationBuilder app)
        {
            using(var scope=app.ApplicationServices.CreateScope())
            {
                var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
                runner.MigrateUp();
            }
            return app;
        }
    }
}
