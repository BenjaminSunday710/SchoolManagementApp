﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Roles.AttachPermissions;
using UserManagement.Application.Commands.Roles.CreateRole;
using UserManagement.Application.Commands.Users.AssignRole;
using UserManagement.Application.Commands.Users.RegisterUser;
using UserManagement.Domain.Permissions;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.ApplicationService.MiddleWares
{
    public static class SeedAdminExtension
    {
        public static async Task SeedAdmin(this IApplicationBuilder app, IConfiguration configuration)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    var session = scope.ServiceProvider.GetService<INHibernateHelper>().OpenSession();

                    if (!session.Query<User>().Any())
                    {
                        var registerUserCommand = new RegisterUserCommand() 
                        { 
                            Email= "sch.admin@schapp.com",
                            FirstName="Admin",
                            LastName="Admin",
                            Password="admin1server*"
                        };
                        var registerUserResponse = await mediator.ExecuteCommandAsync<RegisterUserCommand, RegisterUserCommandHandler, UserManagementDbContext, CommandResponse>(registerUserCommand);

                        var createRoleCommand = new CreateRoleCommand() { Title = "Admin" };
                        var createRoleResponse = await mediator.ExecuteCommandAsync<CreateRoleCommand, CreateRoleCommandHandler, UserManagementDbContext, CommandResponse>(createRoleCommand);

                        var canAssignPermission = session.Query<Permission>().First(permission => permission.Name == PermissionName.CAN_ATTACH_PERMISSIONS);
                        var canFetchRoles = session.Query<Permission>().First(permission => permission.Name == PermissionName.CAN_FETCH_ROLES);
                        var canFetchUsers = session.Query<Permission>().First(permission => permission.Name == PermissionName.CAN_FETCH_USERS);
                        var canAssignRole = session.Query<Permission>().First(permission => permission.Name == PermissionName.CAN_ASSIGN_ROLE);

                        var attachPermissionCommand = new AttachPermissionsCommand()
                        {
                            PermissionIds = new List<Guid>() { canAssignPermission.Id,canAssignRole.Id,canFetchRoles.Id,
                            canFetchUsers.Id},
                            RoleId = createRoleResponse.Data.Id
                        };

                        var attachPermissionResponse = await mediator.ExecuteCommandAsync<AttachPermissionsCommand, AttachPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(attachPermissionCommand);

                        var assignRoleCommand = new AssignRoleCommand()
                        {
                            RoleId = createRoleResponse.Data.Id,
                            UserId = registerUserResponse.Data.Id
                        };
                        var assignRoleResponse = await mediator.ExecuteCommandAsync<AssignRoleCommand, AssignRoleCommandHandler, UserManagementDbContext, CommandResponse>(assignRoleCommand);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Internal server error, admin persistence failed");
            }
        }
    }
}