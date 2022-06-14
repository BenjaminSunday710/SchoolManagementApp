using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.ApplicationService.Authorizations
{
    public abstract class AttributeAuthourizationHandler<TRequirement,TAttribute>:AuthorizationHandler<TRequirement>
        where TRequirement:IAuthorizationRequirement
        where TAttribute:Attribute
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement)
        {
            var httpContext = context.Resource as HttpContext;
            var displayName = httpContext.GetEndpoint().DisplayName;
            var nameComponents = displayName.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            string controllerName = nameComponents[3];
            var index = nameComponents[4].IndexOf('(');
            string actionName = nameComponents[4].Remove(index - 1);
            var assembly = typeof(Startup).Assembly;
            var allControllerTypes = assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(ControllerBase)));
            var controllerType = allControllerTypes.FirstOrDefault(x => x.FullName.Contains(controllerName));
            var actionMethod = controllerType.GetMethod(actionName);
            var permissionAttribute = actionMethod.GetCustomAttribute<PermissionAttribute>();
            await HandleRequirementAsync(context, requirement, permissionAttribute.Name);
        }

        protected abstract Task HandleRequirementAsync(AuthorizationHandlerContext context, TRequirement requirement, string policy);
    }
}
