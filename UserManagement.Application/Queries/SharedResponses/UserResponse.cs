using System;
using System.Collections.Generic;

namespace UserManagement.Application.Queries.SharedResponses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
    }
}
