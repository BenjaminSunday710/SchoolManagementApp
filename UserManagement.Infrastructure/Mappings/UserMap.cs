﻿using FluentNHibernate.Mapping;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("Users");
            Id(x => x.Id).GeneratedBy.Guid();
            Map(x => x.FirstName);
            Map(x => x.LastName);
            Map(x => x.Email);
            Map(x => x.PasswordHash);
            HasManyToMany(x => x.Roles).Cascade.All().Table("UserRoles");
        }
    }
}
