using NHibernate;
using NHibernate.Linq;
using Shared.Domain.Entities;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Repositories
{
    public class Repository<TEntity, TId> : ReadOnlyRepository<TEntity,TId>, IRepository<TEntity, TId>
        where TEntity : IEntity<TId>
    {
        private ISession _session;

        public Repository(ISession session):base(session)
        {
            _session = session;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _session.SaveAsync(entity);
        }

        public async Task DeleteAsync(TEntity entity)
        {
            await _session.DeleteAsync(entity);
        }

        public async Task<bool> ExistsAsync(Expression<Func<TEntity,bool>> predicate)
        {
            return await _session.Query<TEntity>().AnyAsync<TEntity>(predicate);
        }

        public async Task UpdateAsync(TEntity entity, TId id)
        {
            await _session.UpdateAsync(entity, id);
        }
    }
}
