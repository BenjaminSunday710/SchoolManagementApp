using System;
using UserManagement.Domain.Users;

namespace UserManagement.Application.Commands.Users.AuthenticateUser
{
    public class AuthenticatedUserResponse
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
