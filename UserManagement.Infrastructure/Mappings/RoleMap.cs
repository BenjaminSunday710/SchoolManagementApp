using Shared.Infrastructure.Mappings;
using System;
using UserManagement.Domain.Roles;

namespace UserManagement.Infrastructure.Mappings
{
    public class RoleMap : BaseMap<Guid, Role>
    {
        public RoleMap()
        {
            Table("Roles");
            Map(x => x.Title);
            HasManyToMany(x => x.Users).Inverse().Cascade.All().Table("UserRoles");
            HasManyToMany(x => x.Permissions).Cascade.All().Table("RolePermissions");
        }
    }
}
