using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Shared.Domain.Entities;

namespace Shared.Infrastructure.Repositories
{
    public interface IReadOnlyRepository<TEntity,TId>
        where TEntity:IEntity<TId>
    {
        Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TId id);
        IReadOnlyRepository<TEntity, TId> Init();
    }
}
