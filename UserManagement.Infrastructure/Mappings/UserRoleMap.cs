using Shared.Infrastructure.Mappings;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Mappings
{
    public class UserRoleMap:BaseMap<int,UserRole>
    {
        public UserRoleMap()
        {
            Table("UserRoles");
            References(x => x.Role);
            References(x => x.User);
        }
    }
}
