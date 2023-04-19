using Microsoft.AspNetCore.Mvc;
using SchoolManagementAppApi.ApplicationService;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Roles.CreateRole;
using UserManagement.Application.Queries.Roles.FetchRole;
using UserManagement.Application.Queries.Roles.FetchRoles;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Roles;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("roles")]
    public class RoleController:ApiController
    {
        public RoleController(IMediator mediator) : base(mediator) { }
       
        [HttpPost("create")]
        [Permission(PermissionName.CAN_CREATE_ROLE)]
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateRoleCommand, CreateRoleCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet]
        //[Permission(PermissionName.CAN_FETCH_ROLES)]
        public async Task<IActionResult> FetchRoles()
        {
            var response = await Mediator.SendQueryAsync<Role, FetchRolesQueryHandler, List<RoleResponse>>();
            return response.ResponseResult();
        }

        [HttpGet("{userId}")]
        //[Permission(PermissionName.CAN_FETCH_USER_ROLES)]
        public async Task<IActionResult> FetchRole(Guid userId)
        {
            var query = new FetchRoleQuery() { UserId = userId };
            var response = await Mediator.SendQueryAsync<Role, FetchRoleQuery, FetchRoleQueryHandler, List<RoleResponse>>(query);
            return response.ResponseResult();
        }

    }
}
