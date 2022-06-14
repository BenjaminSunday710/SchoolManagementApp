using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Roles;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Roles.FetchRole
{
    public class FetchRoleQueryHandler : QueryHandler<Role, Guid, List<RoleResponse>, FetchRoleQuery>
    {
        public override async Task<ActionResult<List<RoleResponse>>> HandleAsync(FetchRoleQuery query, CancellationToken cancellationToken = default)
        {
            var roles = await QueryContext.FindAsync(role => role.Users.Any(user => user.Id == query.UserId));

            var response = new List<RoleResponse>();
            foreach (var role in roles)
            {
                response.Add(new RoleResponse()
                {
                    Id = role.Id,
                    Title = role.Title
                });
            }

            return OperationResult.Successful(response);
        }
    }
}
