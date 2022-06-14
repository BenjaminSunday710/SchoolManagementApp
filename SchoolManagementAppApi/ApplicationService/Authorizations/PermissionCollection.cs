using System.Collections.Generic;

namespace SchoolManagementAppApi.ApplicationService.Authorizations
{
    public class PermissionCollection
    {
        public PermissionCollection()
        {
            Permissions = new List<string>();
        }
        public List<string> Permissions { get; set; }
    }
}
