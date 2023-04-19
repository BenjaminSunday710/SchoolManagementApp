using System;

namespace UserManagement.Domain.Users
{
    public class UserDto
    {
        public  Guid UserId { get; set; }
        public  string FirstName { get; set; }
        public  string LastName { get; set; }
        public  string Email { get; set; }
    }
}
