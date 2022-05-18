using NHibernate;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Infrastructure.Context
{
    public abstract class DbContext : IContext
    {
        public abstract Task<ActionResult> CommitAsync();
        public abstract void Setup(ISession session);
    }
}
