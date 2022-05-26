using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using UserManagement.Domain.Permissions;
using UserManagement.Domain.Users;

namespace UserManagement.Domain.Roles
{
    public class Role:BaseEntity<Guid>
    {
        protected Role() { }

        public Role(string title)
        {
            Title = title;
        }

        public virtual void AssignPermission(Permission permission)
        {
            _permissions.Add(permission);
        }

        public virtual void DenyPermission(Permission permission)
        {
            _permissions.Remove(permission);
        }

        public virtual string Title { get; protected set; }


        private ISet<User> _users = new HashSet<User>();
        public virtual IEnumerable<User> Users => _users;


        private ISet<Permission> _permissions = new HashSet<Permission>();
        public virtual IEnumerable<Permission> Permissions => _permissions;
    }
}
