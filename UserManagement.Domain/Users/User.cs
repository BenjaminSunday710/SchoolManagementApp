using Microsoft.AspNetCore.Identity;
using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using UserManagement.Domain.Roles;

namespace UserManagement.Domain.Users
{
    public class User:IdentityUser<Guid>,IEntity<Guid>
    {
        protected User() { }

        public User(string firstName, string lastName, string email,string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PasswordHash = password;
        }

        public virtual void AssignRole(Role role)
        {
            _roles.Add(role);
        }

        public virtual void ResignRole(Role role)
        {
            _roles.Remove(role);
        }
        public virtual void ClearRoles()
        {
            _roles.Clear();
        }

        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual TokenManager TokenManager { get; set; }

        private ISet<Role> _roles = new HashSet<Role>();
        public virtual IEnumerable<Role> Roles => _roles;
    }
}
