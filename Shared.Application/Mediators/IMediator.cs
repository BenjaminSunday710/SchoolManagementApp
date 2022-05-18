using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.ArchitectureBuilder.Queries;
using Shared.Domain.Entities;
using Shared.Infrastructure.Context;
using System;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Application.Mediator
{
    public interface IMediator
    {
        Task<ActionResult<TResponse>> ExecuteCommandAsync<TCommand, TCommandHandler, TDbContext, TResponse>(TCommand command)
            where TCommand : Command
            where TCommandHandler : CommandHandler<TCommand, TDbContext, TResponse>
            where TResponse : class
            where TDbContext : DbContext, new();

        Task<ActionResult<TResponse>> SendQueryAsync<TEntity,TQuery, TQueryHandler, TResponse>(TQuery query)
             where TQuery : Query
            where TEntity:BaseEntity<int>
             where TQueryHandler : QueryHandler<TEntity, int, TResponse, TQuery>
             where TResponse : class;

        Task<ActionResult<TResponse>> SendQueryAsync<TEntity,TQueryHandler, TResponse>()
            where TEntity:BaseEntity<int>
            where TQueryHandler : QueryHandler<TEntity, int, TResponse>
            where TResponse : class;

    }
}
