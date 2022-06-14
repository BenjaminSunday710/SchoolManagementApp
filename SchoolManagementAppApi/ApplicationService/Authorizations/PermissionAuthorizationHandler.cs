using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using UserManagement.Domain.Roles;

namespace SchoolManagementAppApi.ApplicationService.Authorizations
{
    public class PermissionAuthorizationHandler : AttributeAuthourizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        public PermissionAuthorizationHandler() { }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, string policy)
        {
            if (!Authorize(context.User, policy)) return Task.CompletedTask;
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        private bool Authorize(ClaimsPrincipal principal, string permission)
        {
            var roleString = principal.Claims.First(claim => claim.Type == "Roles").Value;
            var userRoles = JsonConvert.DeserializeObject<List<RoleString>>(roleString);

            return userRoles.Any(role => role.Permissions.Contains(permission));
        }
    }
}
