using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Permissions;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Permissions.FetchPermissions
{
    public class FetchPermissionsQueryHandler : QueryHandler<Permission, Guid, List<PermissionResponse>>
    {
        public override async Task<ActionResult<List<PermissionResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var permissions = await QueryContext.GetAllAsync();
            var permissionResponses = new List<PermissionResponse>();

            foreach (var permission in permissions)
            {
                permissionResponses.Add(new PermissionResponse()
                {
                    Name = permission.Name,
                    Id = permission.Id
                });
            }
            return OperationResult.Successful(permissionResponses);   
        }
    }
}
