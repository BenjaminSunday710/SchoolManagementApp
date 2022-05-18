using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Shared.Domain.Entities;

namespace Shared.Infrastructure.Repositories
{
    public class ReadOnlyRepository<TEntity,TId>:IReadOnlyRepository<TEntity,TId>
        where TEntity:BaseEntity<TId>
    {
        private ISession _session;

        public ReadOnlyRepository(ISession session)
        {
            _session = session;
        }

        public async Task<List<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _session.Query<TEntity>().Where(predicate).ToListAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _session.Query<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            return await _session.GetAsync<TEntity>(id);
        }

        public IReadOnlyRepository<TEntity, TId> Init()
        {
            return this;
        }
    }
}
