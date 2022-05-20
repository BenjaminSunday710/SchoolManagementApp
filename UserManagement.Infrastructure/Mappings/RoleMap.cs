using Shared.Infrastructure.Mappings;
using UserManagement.Domain.Roles;

namespace UserManagement.Infrastructure.Mappings
{
    public class RoleMap : BaseMap<int, Role>
    {
        public RoleMap()
        {
            Table("Roles");
            Map(x => x.Title);
            HasMany(x => x.Users).Cascade.AllDeleteOrphan().Inverse();
            HasMany(x => x.Permissions).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
