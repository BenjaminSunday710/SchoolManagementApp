using Shared.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Repositories
{
    public interface IRepository<TEntity, TId> :IReadOnlyRepository<TEntity,TId>
        where TEntity : BaseEntity<TId>
    {
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateAsync(TEntity entity, TId id);
    }
}
