using System;

namespace UserManagement.Domain.Users
{
    public class TokenManager
    {
        public virtual string RefreshToken { get; set; }
        public virtual DateTime RefreshTokenExpiryToken { get; set; }
    }
}