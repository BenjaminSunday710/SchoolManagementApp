using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using System.Reflection;

namespace Shared.Infrastructure
{
    public static class NHibernateUtils
    {
        public static ISessionFactory GetSessionFactory(string connectionString, Assembly assembly)
        {
            if(sessionFactoryInstance == null)
            {
                sessionFactoryInstance = BuildSessionFactory(connectionString,assembly);   
            }
            return sessionFactoryInstance;
        }

        private static ISessionFactory BuildSessionFactory(string connectionString, Assembly assembly)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssembly(assembly))
                .ExposeConfiguration(cfg =>
                {
                    cfg.SetProperty(NHibernate.Cfg.Environment.CommandTimeout, "5000");
                });

            return configuration.BuildSessionFactory();
        }

        private static ISessionFactory sessionFactoryInstance;
    }
}
