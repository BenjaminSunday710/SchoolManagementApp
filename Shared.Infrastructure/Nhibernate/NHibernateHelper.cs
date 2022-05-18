using NHibernate;
using System.Reflection;

namespace Shared.Infrastructure
{
    public class NHibernateHelper : INHibernateHelper
    {
        public NHibernateHelper(string connectionString, Assembly assembly)
        {
            sessionFactory = NHibernateUtils.GetSessionFactory(connectionString,assembly);
        }

        public ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }

        private ISessionFactory sessionFactory;
    }
}
