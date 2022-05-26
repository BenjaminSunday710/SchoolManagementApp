using Shared.Infrastructure.Mappings;
using System;
using UserManagement.Domain.Permissions;

namespace UserManagement.Infrastructure.Mappings
{
    public class PermissionMap : BaseMap<Guid, Permission>
    {
        public PermissionMap()
        {
            Table("Permissions");
            Map(x => x.Name);
            HasManyToMany(x => x.Roles).Inverse().Cascade.All().Table("RolePermissions");
        }
    }
}
