using System;
using System.Collections.Generic;
using UserManagement.Domain.Roles;
using UserManagement.Domain.Users;

namespace UserManagement.Application.Commands.Users.AuthenticateUser
{
    public class AuthenticatedUserResponse
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public IEnumerable<Role> Roles { get; set; }
    }
}
