using System;
using System.Collections.Generic;

namespace UserManagement.Domain.Users
{
    public interface IUserIdentity
    {
        string Email { get; set; }
        int UserId { get; set; }
        IEnumerable<string> Roles { get; set; }
    }
}
