using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Infrastructure.Context
{
    public interface IContext
    {
        Task<ActionResult> CommitAsync();
    }
}
