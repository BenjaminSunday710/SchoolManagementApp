using System;
using System.Collections.Generic;
using UserManagement.Domain.Roles;

namespace UserManagement.Domain.Users
{
    public interface IUserIdentity
    {
        string Email { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        Guid UserId { get; set; }
    }
}
