using System.Collections.Generic;

namespace UserManagement.Domain.Roles
{
    public class RoleString
    {
        public string Title { get; set; }
        public IEnumerable<string> Permissions { get; set; }
    }
}
