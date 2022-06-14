using NHibernate;

namespace Shared.Infrastructure
{
    public interface INHibernateHelper
    {
        ISession OpenSession();
        void CloseSession();
    }
}
