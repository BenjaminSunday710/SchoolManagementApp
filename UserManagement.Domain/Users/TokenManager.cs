using System;

namespace UserManagement.Domain.Users
{
    public class TokenManager
    {
        protected TokenManager() { }
        public TokenManager(string token, DateTime refreshTokenExpiryTime)
        {
            RefreshToken = token;
            RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        public virtual string RefreshToken { get; protected set; }
        public virtual DateTime RefreshTokenExpiryTime { get; protected set; }
    }
}