using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Roles;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Roles.FetchRoles
{
    public class FetchRolesQueryHandler : QueryHandler<Role, Guid, List<RoleResponse>>
    {
        public override async Task<ActionResult<List<RoleResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var roles = await QueryContext.GetAllAsync();
            var response = new List<RoleResponse>();

            foreach (var role in roles)
            {
                response.Add(new RoleResponse()
                {
                    Title = role.Title,
                    Id = role.Id
                });
            }
            return OperationResult.Successful(response);
        }
    }
}
