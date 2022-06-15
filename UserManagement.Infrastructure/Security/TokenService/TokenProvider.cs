using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
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

        public ClaimsPrincipal ProvidePrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;

        }

        public string ProvideToken(User user)
        {
            var token = JwtTokenGenerator.GenerateToken(user,_jwtSettings);
            return token;
        }

        public string ProvideRefreshToken()
        {
            var randomNumber = new byte[32];
            using(var randNumGen = RandomNumberGenerator.Create())
            {
                randNumGen.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        //public User ValidateToken(string token)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
