using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Roles;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Roles.FetchRolePermissions
{
    public class FetchRolePermissionsQueryHandler : QueryHandler<Role, Guid, List<PermissionResponse>, FetchRolePermissionsQuery>
    {
        public override async Task<ActionResult<List<PermissionResponse>>> HandleAsync(FetchRolePermissionsQuery query, CancellationToken cancellationToken = default)
        {
            var role = await QueryContext.GetByIdAsync(query.RoleId);
            var responses = new List<PermissionResponse>();
            foreach (var permission in role.Permissions)
            {
                responses.Add(new PermissionResponse()
                {
                    Name = permission.Name
                });
            }
            return OperationResult.Successful(responses);
        }
    }
}
