using FluentNHibernate.Testing.Values;
using NHibernate.Util;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.ArchitectureBuilder.Queries;
using Shared.Application.Mediator;
using Shared.Application.Mediators.DependencyManagers;
using Shared.Domain.Entities;
using Shared.Infrastructure;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace Shared.Application.Mediators
{
    public class Mediator : IMediator
    {
        public Mediator(IServiceProvider serviceProvider)
        {
            sessionProvider = (INHibernateHelper)serviceProvider.GetService(typeof(INHibernateHelper));
            _serviceProvider = serviceProvider;
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
                var handler = HandlerActivator.Activate<TCommandHandler>(_serviceProvider);
                handler.ManagePropertyInjection(_serviceProvider);

                var dbContext = (TDbContext)Activator.CreateInstance(typeof(TDbContext));
                var session = sessionProvider.OpenSession();
                dbContext.Setup(session);
                handler.Context = dbContext;
                //handler.ServiceProvider = _serviceProvider;
                return await handler.HandleAsync(command);
            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError("unable to handle the request");
            }
            finally
            {
                sessionProvider.CloseSession();
            }
        }

        private object[] GetParameters(ConstructorInfo parameteredCtor)
        {
            var paramInfos=parameteredCtor.GetParameters();
            object[] parameters = new object[paramInfos.Length];

            for (int i = 0; i < paramInfos.Length; i++)
            {
                var paramType = paramInfos[i].GetType();
                if (!paramType.IsInterface) throw new ArgumentException("invalid constructor parameter");

                var paramConcType = _serviceProvider.GetService(paramType);
                if (paramConcType == null) throw new ArgumentException("required type is not registered");
                parameters[i] = paramConcType;
            }
            return parameters;
        }

        public async Task<ActionResult<TResponse>> SendQueryAsync<TEntity,TQueryHandler, TResponse>()
            where TEntity:IEntity<Guid>
            where TQueryHandler : QueryHandler<TEntity, Guid, TResponse>
            where TResponse : class
        {
            try
            {
                var handler = HandlerActivator.Activate<TQueryHandler>(_serviceProvider);
                handler.ManagePropertyInjection(_serviceProvider);

                var session = sessionProvider.OpenSession();
                handler.QueryContext = new ReadOnlyRepository<TEntity, Guid>(session);
                return await handler.HandleAsync();
            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError("unable to handle the request");
            }
            finally
            {
                sessionProvider.CloseSession();
            }
        }

        public async Task<ActionResult<TResponse>> SendQueryAsync<TEntity,TQuery, TQueryHandler, TResponse>(TQuery query)
            where TEntity : IEntity<Guid>
            where TQuery :Query
            where TQueryHandler : QueryHandler<TEntity, Guid, TResponse, TQuery>
            where TResponse : class
        {
            if (!query.IsValid) return ActionResult<TResponse>.Failed();
            try
            {
                var handler = HandlerActivator.Activate<TQueryHandler>(_serviceProvider);
                handler.ManagePropertyInjection(_serviceProvider);

                var session = sessionProvider.OpenSession();
                handler.QueryContext = new ReadOnlyRepository<TEntity, Guid>(session);
                return await handler.HandleAsync(query);
            }
            catch (Exception ex)
            {
                return ActionResult<TResponse>.Failed().AddError("unable to handle the request");
            }
            finally
            {
                sessionProvider.CloseSession();
            }
        }

        private INHibernateHelper sessionProvider;
        private IServiceProvider _serviceProvider;
    }
}
