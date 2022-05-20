using Shared.Infrastructure.Mappings;
using UserManagement.Domain.Roles;

namespace UserManagement.Infrastructure.Mappings
{
    public class RolePermissionMap : BaseMap<int, RolePermission>
    {
        public RolePermissionMap()
        {
            Table("RolePermissions");
            References(x => x.Permission);
            References(x => x.Role);
        }
    }
}
