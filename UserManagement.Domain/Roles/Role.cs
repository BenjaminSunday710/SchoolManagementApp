using Shared.Domain.Entities;
using System.Collections.Generic;
using UserManagement.Domain.Users;

namespace UserManagement.Domain.Roles
{
    public class Role:BaseEntity<int>
    {
        protected Role() { }

        public Role(string title)
        {
            Title = title;
        }

        public virtual void HasPermission(RolePermission permission)
        {
            permission.Role = this;
            _permissions.Add(permission);
        }

        public virtual void DenyPermission(RolePermission permission)
        {
            permission.DetachRole();
            _permissions.Remove(permission);
        }

        protected internal virtual void HasUser(UserRole user)
        {
            user.Role = this;
            _users.Add(user);
        }

        protected internal virtual void RemoveUser(UserRole user)
        {
            user.Role = null;
            _users.Remove(user);
        }

        public virtual string Title { get; protected set; }


        private ISet<UserRole> _users = new HashSet<UserRole>();
        public virtual IEnumerable<UserRole> Users => _users;


        private ISet<RolePermission> _permissions = new HashSet<RolePermission>();
        public IEnumerable<RolePermission> Permissions => _permissions;
    }
}
