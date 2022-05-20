using System.Threading.Tasks;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.UserIdentity;

namespace UserManagement.Application.Commands.Users.Services
{
    public interface IAuthenticationService
    {
        public Task<User> AuthenticateAsync(UserIdentity userIdentity);
        public Task<User> RegisterAsync(User user);
    }
}
