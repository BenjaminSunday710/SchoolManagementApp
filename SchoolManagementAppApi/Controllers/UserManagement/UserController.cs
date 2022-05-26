using Microsoft.AspNetCore.Mvc;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Users.AssignRole;
using UserManagement.Application.Commands.Users.RegisterUser;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Application.Queries.Users.FetchUserRoles;
using UserManagement.Application.Queries.Users.FetchUsers;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("users")]
    public class UserController:ApiController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
        {
            var registerAction = await Mediator.ExecuteCommandAsync<RegisterUserCommand, RegisterUserCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return registerAction.ResponseResult();
        }

        [HttpGet()]
        public async Task<IActionResult> FetchUsers()
        {
            var response = await Mediator.SendQueryAsync<User, FetchUsersQueryHandler, List<UserResponse>>();
            return response.ResponseResult();
        }

        [HttpPut("assign-role")]
        public async Task<IActionResult> AssignRole(AssignRoleCommand command)
        {
            var assignAction = await Mediator.ExecuteCommandAsync<AssignRoleCommand, AssignRoleCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return assignAction.ResponseResult();
        }

        [HttpGet("{id}/roles")]
        public async Task<IActionResult> FetchRolePermissions(Guid id)
        {
            var query = new FetchUserRolesQuery() { UserId = id };
            var response = await Mediator.SendQueryAsync<User, FetchUserRolesQuery, FetchUserRolesQueryHandler, List<RoleResponse>>(query);
            return response.ResponseResult();
        }
    }
}
