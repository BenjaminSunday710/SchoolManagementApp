using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Roles;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace UserManagement.Application.Queries.Users.FetchUsers
{
    public class FetchUsersQueryHandler : QueryHandler<User, Guid, List<UserResponse>>
    {
        public override async Task<ActionResult<List<UserResponse>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var users = await QueryContext.GetAllAsync();
            var response = new List<UserResponse>();

            foreach (var user in users)
            {
                response.Add(new UserResponse()
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id
                });
            }
            return OperationResult.Successful(response);
        }

        private IEnumerable<RoleResponse> CreateUserRolesDto(IEnumerable<Role> roles)
        {
            var roleDtos = new List<RoleResponse>();
            foreach (var role in roles)
            {
                roleDtos.Add(new RoleResponse()
                {
                    Title = role.Title,
                    Id=role.Id
                });
            }
            return roleDtos;
        }
    }
}
