using Shared.Infrastructure.Mappings;
using System;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Mappings
{
    public class UserMap : BaseMap<Guid, User>
    {
        public UserMap()
        {
            Table("Users");
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.Password);
            HasManyToMany(x => x.Roles).Cascade.All().Table("UserRoles");
        }
    }
}
