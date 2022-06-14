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
            session= sessionFactory.OpenSession();
            return session;
        }

        public void CloseSession()
        {
            session.Close();
        }

        private ISessionFactory sessionFactory;
        private ISession session;
    }
}
