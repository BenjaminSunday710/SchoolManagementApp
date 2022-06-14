using Microsoft.AspNetCore.Authorization;

namespace SchoolManagementAppApi.ApplicationService.Authorizations
{
    public class PermissionAuthorizationRequirement:IAuthorizationRequirement
    {
        public PermissionAuthorizationRequirement(string permission)
        {
            Permission = permission;
        }
        public string Permission { get; set; }
    }
}
