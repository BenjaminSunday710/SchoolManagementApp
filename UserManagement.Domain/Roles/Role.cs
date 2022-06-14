using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public virtual void AllowPermission(Permission permission)
        {
            _permissions.Add(permission);
        }

        public virtual void DenyPermission(Permission permission)
        {
            _permissions.Remove(permission);
        }

        public virtual RoleString GetRoleString()
        {
            var permissionNames = this.Permissions.Select(p => p.Name);
            return new RoleString()
            {
                Title = this.Title,
                Permissions = permissionNames
            };
        }

        public virtual string Title { get; protected set; }

        private ISet<User> _users = new HashSet<User>();
        public virtual IEnumerable<User> Users => _users;

        private ISet<Permission> _permissions = new HashSet<Permission>();
        public virtual IEnumerable<Permission> Permissions => _permissions;
    }
}
