using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Security.TokenService
{
    public interface ITokenProvider
    {
        public string ProvideToken(User user);
    }
}
