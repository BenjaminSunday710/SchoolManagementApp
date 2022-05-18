using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.ArchitectureBuilder.Queries;
using Shared.Application.Mediator;
using Shared.Domain.Entities;
using Shared.Infrastructure;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Application.Mediators
{
    public class Mediator : IMediator
    {
        public Mediator(INHibernateHelper nHibernateHelper)
        {
            sessionProvider = nHibernateHelper;
        }
        public async Task<ActionResult<TResponse>> ExecuteCommandAsync<TCommand, TCommandHandler,TDbContext, TResponse>(TCommand command)
            where TCommand:Command
            where TDbContext:DbContext,new()
            where TCommandHandler:CommandHandler<TCommand, TDbContext, TResponse>
            where TResponse : class
        {
            if (!command.IsValid) return ActionResult<TResponse>.Failed(ErrorCode.BadRequest);
            try
            {
                var handler = (TCommandHandler)Activator.CreateInstance(typeof(TCommandHandler));
                var dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext));
                var session = sessionProvider.OpenSession();
                dbContext.Setup(session);
                handler.Context = dbContext;
                return await handler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError("unable to handle the request");
            }
        }

        public async Task<ActionResult<TResponse>> SendQueryAsync<TEntity,TQueryHandler, TResponse>()
            where TEntity:BaseEntity<int>
            where TQueryHandler : QueryHandler<TEntity, int, TResponse>
            where TResponse : class
        {
            try
            {
                var handler = (TQueryHandler)Activator.CreateInstance(typeof(TQueryHandler));
                var session = sessionProvider.OpenSession();
                handler.QueryContext = new ReadOnlyRepository<TEntity, int>(session);
                return await handler.HandleAsync();
            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError("unable to handle the request");
            }
        }

        public async Task<ActionResult<TResponse>> SendQueryAsync<TEntity,TQuery, TQueryHandler, TResponse>(TQuery query)
            where TEntity : BaseEntity<int>
            where TQuery :Query
            where TQueryHandler : QueryHandler<TEntity, int, TResponse, TQuery>
            where TResponse : class
        {
            if (!query.IsValid) return ActionResult<TResponse>.Failed();
            try
            {
                var handler = (TQueryHandler)Activator.CreateInstance(typeof(TQueryHandler));
                var session = sessionProvider.OpenSession();
                handler.QueryContext = new ReadOnlyRepository<TEntity, int>(session);
                return await handler.HandleAsync(query);
            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError("unable to handle the request");
            }
        }

        private INHibernateHelper sessionProvider;
    }
}
