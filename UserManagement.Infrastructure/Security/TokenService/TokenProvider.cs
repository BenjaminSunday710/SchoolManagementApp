using Microsoft.Extensions.Options;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Security.TokenService
{
    public class TokenProvider : ITokenProvider
    {
        private readonly JwtSettings _jwtSettings;

        public TokenProvider(IOptions<JwtSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value;
        }

        public string ProvideToken(User user)
        {
            var token = JwtTokenGenerator.GenerateToken(user,_jwtSettings);
            return token;
        }
    }
}
