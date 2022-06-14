using System.Threading.Tasks;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByEmailAsync(string email);
    }
}
