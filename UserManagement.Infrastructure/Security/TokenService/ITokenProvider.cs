using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Security.TokenService
{
    public interface ITokenProvider
    {
        public string ProvideToken(User user);
        public User ValidateToken(string token);
    }
}
