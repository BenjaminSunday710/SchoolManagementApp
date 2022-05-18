using Shared.Domain.Entities;
using System.Collections.Generic;
using UserManagement.Domain.Roles;

namespace UserManagement.Domain.Permissions
{
    public class Permission:BaseEntity<int>
    {
        protected Permission() { }

        public Permission(string name)
        {
            Name = name;
        }

        protected internal virtual void AddRole(RolePermission role)
        {
            role.Permission = this;
            _roles.Add(role);
        }

        protected internal virtual void RemoveRole(RolePermission role)
        {
            role.Permission = null;
            _roles.Remove(role);
        }

        public virtual string Name { get; protected set; }


        private ISet<RolePermission> _roles = new HashSet<RolePermission>();
        public virtual IEnumerable<RolePermission> Roles => _roles;
    }
}
