using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Collections.Generic;
using System.Reflection;

namespace Shared.Infrastructure
{
    public static class NHibernateUtils
    {
        public static ISessionFactory GetSessionFactory(string connectionString, List<Assembly> assemblies)
        {
            if(sessionFactoryInstance == null)
            {
                sessionFactoryInstance = BuildSessionFactory(connectionString, assemblies);   
            }
            return sessionFactoryInstance;
        }

        private static ISessionFactory BuildSessionFactory(string connectionString, List<Assembly> mappingAssemblies)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => mappingAssemblies.ForEach(assembly => m.FluentMappings.AddFromAssembly(assembly)))
                .ExposeConfiguration(cfg =>
                {
                    cfg.SetProperty(NHibernate.Cfg.Environment.CommandTimeout, "5000");
                });

            return configuration.BuildSessionFactory();
        }

        private static ISessionFactory sessionFactoryInstance;
    }
}
