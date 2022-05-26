using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Users.FetchUserRoles
{
    public class FetchUserRolesQueryHandler : QueryHandler<User, Guid, List<RoleResponse>,FetchUserRolesQuery>
    {
        public override async Task<ActionResult<List<RoleResponse>>> HandleAsync(FetchUserRolesQuery query, CancellationToken cancellationToken = default)
        {
            var roleResponses = new List<RoleResponse>();
            var user = await QueryContext.GetByIdAsync(query.UserId);
            foreach (var role in user.Roles)
            {
                roleResponses.Add(new RoleResponse
                {
                    Title = role.Title,
                    Id = role.Id
                });
            }
            return OperationResult.Successful(roleResponses);
        }
    }
}
