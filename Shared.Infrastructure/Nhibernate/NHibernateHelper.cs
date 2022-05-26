using NHibernate;
using System.Collections.Generic;
using System.Reflection;

namespace Shared.Infrastructure
{
    public class NHibernateHelper : INHibernateHelper
    {
        public NHibernateHelper(string connectionString, List<Assembly> mappingAssemblies)
        {
            sessionFactory = NHibernateUtils.GetSessionFactory(connectionString, mappingAssemblies);
        }

        public ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        private ISessionFactory sessionFactory;
    }
}
