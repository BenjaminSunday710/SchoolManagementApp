using Shared.Domain.Entities;
using System;
using System.Collections.Generic;
using UserManagement.Domain.Roles;

namespace UserManagement.Domain.Permissions
{
    public class Permission:BaseEntity<Guid>
    {
        protected Permission() { }

        public Permission(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Name: {Name}";
        }

        public virtual string Name { get; protected set; }

        private ISet<Role> _roles = new HashSet<Role>();
        public virtual IEnumerable<Role> Roles => _roles;
    }
}
