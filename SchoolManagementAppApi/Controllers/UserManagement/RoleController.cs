using Microsoft.AspNetCore.Mvc;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Roles.AssignPermissions;
using UserManagement.Application.Commands.Roles.CreateRole;
using UserManagement.Application.Queries.Roles.FetchRolePermissions;
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
        public async Task<IActionResult> CreateRole(CreateRoleCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateRoleCommand, CreateRoleCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet]
        public async Task<IActionResult> FetchRoles()
        {
            var response = await Mediator.SendQueryAsync<Role, FetchRolesQueryHandler, List<RoleResponse>>();
            return response.ResponseResult();
        }

        [HttpPut("assign-permissions")]
        public async Task<IActionResult> AssignPermissions(AssignPermissionsCommand command)
        {
            var assignAction = await Mediator.ExecuteCommandAsync<AssignPermissionsCommand, AssignPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return assignAction.ResponseResult();
        }

        [HttpGet("{id}/permissions")]
        public async Task<IActionResult> FetchRolePermissions(Guid id)
        {
            var query = new FetchRolePermissionsQuery() { RoleId = id };
            var response = await Mediator.SendQueryAsync<Role, FetchRolePermissionsQuery, FetchRolePermissionsQueryHandler, List<PermissionResponse>>(query);
            return response.ResponseResult();
        }
    }
}
