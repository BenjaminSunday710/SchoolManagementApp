using Shared.Domain.Entities;
using System.Collections.Generic;

namespace UserManagement.Domain.Users
{
    public class User:BaseEntity<int>
    {
        protected User() { }

        public User(string firstName, string lastName, string email, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
        }

        protected internal virtual void AssignRole(UserRole role)
        {
            role.User = this;
            _roles.Add(role);
        }

        protected internal virtual void ResignRole(UserRole role)
        {
            role.DetachUser();
            _roles.Remove(role);
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        private ISet<UserRole> _roles = new HashSet<UserRole>();
        public virtual IEnumerable<UserRole> Roles => _roles;
    }
}
