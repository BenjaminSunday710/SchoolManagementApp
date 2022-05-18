using Shared.Infrastructure.Mappings;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Mappings
{
    public class UserMap : BaseMap<int,User>
    {
        public UserMap()
        {
            Table("Users");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.Password);
            HasMany(x => x.Roles).Cascade.AllDeleteOrphan().Inverse();
        }
    }
}
