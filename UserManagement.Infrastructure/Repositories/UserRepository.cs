using NHibernate;
using NHibernate.Linq;
using Shared.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository:Repository<User, Guid>,IUserRepository
    {
        public UserRepository(ISession session) : base(session)
        {
            _session = session;
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _session.Query<User>().FirstOrDefaultAsync(user => user.Email == email);
        }

        private readonly ISession _session;
    }
}
