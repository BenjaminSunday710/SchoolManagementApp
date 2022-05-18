using Shared.Domain.Entities;
using Shared.Infrastructure.Repositories;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Application.ArchitectureBuilder.Queries
{
    public abstract class QueryHandler<TEntity, TId, TResponse> 
        where TEntity:BaseEntity<TId>
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(CancellationToken cancellationToken = default);
        public Operation<TResponse> OperationResult = new Operation<TResponse>();
        public IReadOnlyRepository<TEntity,TId> QueryContext { get; internal set; }
    }

    public abstract class QueryHandler<TEntity, TId, TResponse, TQuery> 
        where TQuery:Query
        where TEntity : BaseEntity<TId>
    {
        public abstract Task<ActionResult<TResponse>> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
        public Operation<TResponse> OperationResult = new Operation<TResponse>();
        public IReadOnlyRepository<TEntity, TId> QueryContext { get; internal set; } 
    }
}
