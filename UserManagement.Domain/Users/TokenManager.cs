using System;

namespace UserManagement.Domain.Users
{
    public class TokenManager
    {
        protected TokenManager() { }
        public TokenManager(string token, DateTime refreshTokenExpiryTime)
        {
            RefreshedToken = token;
            RefreshedTokenExpiryTime = refreshTokenExpiryTime;
        }

        public virtual string RefreshedToken { get; protected set; }
        public virtual DateTime RefreshedTokenExpiryTime { get; protected set; }
    }
}