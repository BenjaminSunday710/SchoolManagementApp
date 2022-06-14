using Microsoft.AspNetCore.Authorization;
using System;

namespace SchoolManagementAppApi.ApplicationService.Authorizations
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method, AllowMultiple =true)]
    public class PermissionAttribute:AuthorizeAttribute
    {
        private const string HAS_PERMISSION_POLICY_NAME = "HasPermission:";
        public PermissionAttribute(string name) => Name = name;

        public string Name 
        {
            get => Policy.Substring(HAS_PERMISSION_POLICY_NAME.Length);
            set
            {
                Policy = $"{HAS_PERMISSION_POLICY_NAME}{value}";
            }
        }
    }
}
