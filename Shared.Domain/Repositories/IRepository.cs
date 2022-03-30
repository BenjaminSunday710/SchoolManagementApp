using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Domain.Repositories
{
    public interface IRepository<T> where T:BaseEntity
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> Find(Func<bool> predicate);
        Task<bool> Exists(Func<bool> predicate);
        Task Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
    }
}
