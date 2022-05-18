using Shared.Domain.Entities;
using UserManagement.Domain.Permissions;

namespace UserManagement.Domain.Roles
{
    public class RolePermission:BaseEntity<int>
    {
        public RolePermission(Role role, Permission permission)
        {
            role.HasPermission(this);
            permission.AddRole(this);
        }

        protected internal virtual void DetachRole()
        {
            this.Role = null;
            this.Permission.RemoveRole(this);
        }

        public Role Role { get; set; }
        public Permission Permission { get; set; }
    }
}
