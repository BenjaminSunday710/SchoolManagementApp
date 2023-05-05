using Microsoft.AspNetCore.Mvc;
using SchoolManagementAppApi.ApplicationService;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Roles.AssignRole;
using UserManagement.Application.Commands.Roles.ResignRole;
using UserManagement.Application.Commands.Users.RemoveUser;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Application.Queries.Users.FetchUsers;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("users")]
    public class UserController:ApiController
    {
        public UserController(IMediator mediator) : base(mediator) { }

        [HttpGet()]
        [Permission(PermissionName.CAN_FETCH_USERS)]
        public async Task<IActionResult> FetchUsers()
        {
            var response = await Mediator.SendQueryAsync<User, FetchUsersQueryHandler, List<UserResponse>>();
            return response.ResponseResult();
        }

        [HttpPut("assign-role")]
        [Permission(PermissionName.CAN_ASSIGN_ROLE)]
        public async Task<IActionResult> AssignRole(AssignRoleCommand command)
        {
            var assignAction = await Mediator.ExecuteCommandAsync<AssignRoleCommand, AssignRoleCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return assignAction.ResponseResult();
        }

        [HttpPut("resign-role")]
        [Permission(PermissionName.CAN_ASSIGN_ROLE)]
        public async Task<IActionResult> ResignRole(ResignRoleCommand command)
        {
            var resignAction = await Mediator.ExecuteCommandAsync<ResignRoleCommand, ResignRoleCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return resignAction.ResponseResult();
        }

        [HttpPut("remove/{userId}")]
        [Permission(PermissionName.CAN_ASSIGN_ROLE)]
        public async Task<IActionResult> RemoveUser(Guid userId)
        {
            var command = new RemoveUserCommand { UserId = userId };
            var resignAction = await Mediator.ExecuteCommandAsync<RemoveUserCommand, RemoveUserCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return resignAction.ResponseResult();
        }
    }
}
