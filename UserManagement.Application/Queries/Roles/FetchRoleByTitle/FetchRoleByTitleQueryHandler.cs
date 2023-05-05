using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Roles;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Roles.FetchRoleByTitle
{
    public class FetchRoleByTitleQueryHandler : QueryHandler<Role, Guid, RoleResponse, FetchRoleByTitleQuery>
    {
        public override async Task<ActionResult<RoleResponse>> HandleAsync(FetchRoleByTitleQuery query, CancellationToken cancellationToken = default)
        {
            var roles = await QueryContext.FindAsync(role => role.Title == query.Title);

            if (!roles.Any()) return OperationResult.Failed($"role with title:{query.Title} not found");

            var response = new RoleResponse
            {
                Id = roles[0].Id,
                Title = roles[0].Title
            };

            return OperationResult.Successful(response);
        }
    }
}
