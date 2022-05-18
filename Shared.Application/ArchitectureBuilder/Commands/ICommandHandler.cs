using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Application.ArchitectureBuilder.Commands
{
    public interface ICommandHandler<TCommand,TResponse>
    {
        Task<ActionResult<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
