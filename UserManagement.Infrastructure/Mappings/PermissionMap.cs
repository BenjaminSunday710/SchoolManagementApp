using Shared.Infrastructure.Mappings;
using UserManagement.Domain.Permissions;

namespace UserManagement.Infrastructure.Mappings
{
    public class PermissionMap:BaseMap<int,Permission>
    {
        public PermissionMap()
        {
            Table("Permissions");
            Map(x => x.Name);
            HasMany(x=>x.Roles).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
