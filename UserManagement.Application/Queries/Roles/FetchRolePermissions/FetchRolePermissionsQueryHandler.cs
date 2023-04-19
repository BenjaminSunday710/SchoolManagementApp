using NHibernate.Util;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Permissions;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Roles.FetchRolePermissions
{
    public class FetchRolePermissionsQueryHandler : QueryHandler<Permission, Guid, List<PermissionResponse>, FetchRolePermissionsQuery>
    {
        public override async Task<ActionResult<List<PermissionResponse>>> HandleAsync(FetchRolePermissionsQuery query, CancellationToken cancellationToken = default)
        {
            var permissions = await QueryContext.FindAsync(permission => permission.Roles.Any(role => role.Id == query.RoleId));
            var responses = new List<PermissionResponse>();
            foreach (var permission in permissions)
            {
                responses.Add(new PermissionResponse()
                {
                    Name = permission.Name,
                    Id=permission.Id
                });
            }
            return OperationResult.Successful(responses);
        }
    }
}
