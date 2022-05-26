using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using UserManagement.Domain.Roles;

namespace UserManagement.Domain.Users
{
    public class User:BaseEntity<Guid>
    {
        protected User() { }

        public User(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public virtual void AssignRole(Role role)
        {
            _roles.Add(role);
        }

        public virtual void ResignRole(Role role)
        {
            _roles.Remove(role);
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }

        private ISet<Role> _roles = new HashSet<Role>();
        public virtual IEnumerable<Role> Roles => _roles;
    }
}
