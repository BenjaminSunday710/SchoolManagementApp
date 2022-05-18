using Microsoft.Extensions.DependencyInjection;
using FluentMigrator.Runner;
using System.Reflection;
using System;
using System.Linq;
using System.Collections.Generic;
using FluentMigrator;

namespace Shared.Infrastructure
{
    public static class MigrationService
    {
        public static IServiceCollection AddMigrationService(this IServiceCollection services, string connectionString)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x.FullName.Contains("Infrastructure")).ToArray();

            services.AddFluentMigratorCore()
                 .ConfigureRunner(config => config
                 .AddSqlServer()
                 .WithGlobalConnectionString(connectionString)
                 .ScanIn(assemblies).For.Migrations())
                 .AddLogging(config => config.AddFluentMigratorConsole());

            return services;
        }
    }
}
