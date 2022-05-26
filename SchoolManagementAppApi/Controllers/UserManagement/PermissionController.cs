using Microsoft.AspNetCore.Mvc;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Permissions.CreatePermission;
using UserManagement.Application.Queries.Permissions.FetchPermissions;
using UserManagement.Application.Queries.SharedResponses;
using UserManagement.Domain.Permissions;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("permissions")]
    public class PermissionController:ApiController
    {
        public PermissionController(IMediator mediator) : base(mediator) { }

        [HttpPost("create")]
        public async Task<IActionResult> CreatePermission(CreatePermissionCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreatePermissionCommand, CreatePermissionCommandHandler, UserManagementDbContext, CommandResponse>(command);
            return Ok(createAction);
        }

        [HttpGet]
        public async Task<IActionResult> FetchPermissions()
        {
            var response=await Mediator.SendQueryAsync<Permission,FetchPermissionsQueryHandler,List< PermissionResponse>> ();
            return Ok(response);
        }
    }
}
