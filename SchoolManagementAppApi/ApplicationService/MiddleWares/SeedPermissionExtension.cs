using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Permissions.CreatePermission;
using UserManagement.Domain.Permissions;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.ApplicationService.MiddleWares
{
    public static class SeedPermissionExtension
    {
        public static async Task SeedPermissions(this IApplicationBuilder app, IConfiguration configuration)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    var session = scope.ServiceProvider.GetService<INHibernateHelper>().OpenSession();

                    var permissionCollection = new PermissionCollection();
                    configuration.GetSection("PermissionCollection").Bind(permissionCollection);

                    var savedPermissions = session.Query<Permission>().ToList();

                    if (!savedPermissions.Any()) await PersistPermissions(permissionCollection.Permissions, mediator);

                    else if(savedPermissions.Count < permissionCollection.Permissions.Count)
                    {
                        var unsavedPermissions = FilterUnsavedPermissions(savedPermissions, permissionCollection.Permissions);

                        await PersistPermissions(unsavedPermissions, mediator);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Internal server error, permissions persistence failed");
            }
        }

        private static List<string> FilterUnsavedPermissions(List<Permission> savedPermissions, List<string> permissionNames)
        {
            var unsavedPermissions = new List<string>();
            foreach (var permissionName in permissionNames)
            {
                if (!savedPermissions.Any(x => x.Name == permissionName)) unsavedPermissions.Add(permissionName);
            }
            return unsavedPermissions;
        }

        private static async Task PersistPermissions(IEnumerable<string> permissions, IMediator mediator)
        {
            foreach (var permissionName in permissions)
            {
                var command = new CreatePermissionCommand() { Name = permissionName };
                var response = await mediator.ExecuteCommandAsync<CreatePermissionCommand, CreatePermissionCommandHandler, UserManagementDbContext, CommandResponse>(command);
            }
        }
    }
}
