using Microsoft.AspNetCore.Mvc;
using SchoolManagementAppApi.ApplicationService;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Permissions.AttachPermissions;
using UserManagement.Application.Commands.Permissions.DenyPermission;
using UserManagement.Application.Queries.Permissions.FetchPermissions;
using UserManagement.Application.Queries.Roles.FetchRolePermissions;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Permissions;
using UserManagement.Domain.Roles;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("permissions")]
    public class PermissionController : ApiController
    {
        public PermissionController(IMediator mediator,IUserIdentity currentUser) : base(mediator) { }

        [HttpPost("attach")]
        //[Permission(PermissionName.CAN_ATTACH_PERMISSIONS)]
        public async Task<IActionResult> AttachPermission(AttachPermissionsCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<AttachPermissionsCommand, AttachPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return Ok(createAction);
        }

        [HttpPost("deny")]
        //[Permission(PermissionName.CAN_ATTACH_PERMISSIONS)]
        public async Task<IActionResult> DenyPermission(DenyPermissionCommand command)
        {
            var updateAction = await Mediator.ExecuteCommandAsync<DenyPermissionCommand, DenyPermissionCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return Ok(updateAction);
        }

        [HttpGet]
        //[Permission(PermissionName.CAN_FETCH_PERMISSIONS)]
        public async Task<IActionResult> FetchPermissions()
        {
            var response=await Mediator.SendQueryAsync<Permission,FetchPermissionsQueryHandler,List< PermissionResponse>> ();
            return Ok(response);
        }

        [HttpGet("{roleId}")]
        //[Permission(PermissionName.CAN_FETCH_ROLE_PERMISSIONS)]
        public async Task<IActionResult> FetchRolePermissions(Guid roleId)
        {
            var query = new FetchRolePermissionsQuery() { RoleId = roleId };
            var response = await Mediator.SendQueryAsync<Permission, FetchRolePermissionsQuery, FetchRolePermissionsQueryHandler, List<PermissionResponse>>(query);
            return response.ResponseResult();
        }
    }
}
