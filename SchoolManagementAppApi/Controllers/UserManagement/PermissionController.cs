using Microsoft.AspNetCore.Mvc;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Roles.AttachPermissions;
using UserManagement.Application.Queries.Permissions.FetchPermissions;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Permissions;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("permissions")]
    public class PermissionController : ApiController
    {
        public PermissionController(IMediator mediator,IUserIdentity currentUser) : base(mediator) { }

        [HttpPost("attach")]
        [Permission(PermissionName.CAN_ATTACH_PERMISSIONS)]
        public async Task<IActionResult> AttachPermission(AttachPermissionsCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<AttachPermissionsCommand, AttachPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return Ok(createAction);
        }

        [HttpGet]
        [Permission(PermissionName.CAN_FETCH_PERMISSIONS)]
        public async Task<IActionResult> FetchPermissions()
        {
            var response=await Mediator.SendQueryAsync<Permission,FetchPermissionsQueryHandler,List< PermissionResponse>> ();
            return Ok(response);
        }
    }
}
