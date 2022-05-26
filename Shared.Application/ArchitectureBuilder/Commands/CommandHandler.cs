using Shared.Infrastructure.Context;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Application.ArchitectureBuilder.Commands
{
    public abstract class CommandHandler<TCommand,TDbContext, TResponse>:ICommandHandler<TCommand,TResponse> 
        where TCommand:Command
        where TDbContext:class,IContext
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TCommand command, CancellationToken cancellationToken = default);

        public TDbContext Context { get; internal set; }
        public Operation<TResponse> OperationResult => new Operation<TResponse>();
        public IServiceProvider ServiceProvider { get; internal set; }
    }
    
}
