using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using Shared.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Permissions.AttachPermissions;
using UserManagement.Application.Commands.Roles.CreateRole;
using UserManagement.Domain.Permissions;
using UserManagement.Domain.Roles;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.ApplicationService.MiddleWares
{
    public static class SeedDefaultRolesExtension
    {
        public static async Task SeedDefaultRoles(this IApplicationBuilder app)
        {
            try
            {
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();
                    var session = scope.ServiceProvider.GetService<INHibernateHelper>().OpenSession();

                    var dbRoleCount = session.Query<Role>().Count();
                    if (dbRoleCount <= 1)
                    {
                        var permissions = session.Query<Permission>().ToList();
                        await PersistAcademicStaffRole(permissions, mediator);
                        await PersistNonAcademicStaffRole(permissions, mediator);
                        await PersistStudentRole(permissions, mediator);
                        await PersistRegistrarRole(permissions, mediator);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Internal server error, default role persistence failed");
            }
        }

        private static async Task PersistRegistrarRole(IEnumerable<Permission> permissions, IMediator mediator)
        {
            var createRoleCommand = new CreateRoleCommand() { Title = "Registrar" };
            var createRoleResponse = await mediator.ExecuteCommandAsync<CreateRoleCommand, CreateRoleCommandHandler, UserManagementDbContext, CommandResponse>(createRoleCommand);

            var canRegisterStudent = permissions.First(permission => permission.Name == PermissionName.CAN_REGISTER_STUDENT);
            var canFetchRoles = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_ROLES);
            var canRegisterNonAcadStaff = permissions.First(permission => permission.Name == PermissionName.CAN_CREATE_NON_ACADEMICSTAFF);
            var canAssignRole = permissions.First(permission => permission.Name == PermissionName.CAN_ASSIGN_ROLE);
            var canRegisterAcadStaffRole = permissions.First(permission => permission.Name == PermissionName.CAN_CREATE_ACADEMICSTAFF);
            var canFetchStudents = permissions.First(Permission => Permission.Name == PermissionName.CAN_FETCH_STUDENTS);
            var canFetchClasses = permissions.First(Permission => Permission.Name == PermissionName.CAN_FETCH_SCHOOLCLASSES);
            var canFetchSchools = permissions.First(Permission => Permission.Name == PermissionName.CAN_FETCH_SCHOOLS);
            var canFetchSubjects = permissions.First(Permission => Permission.Name == PermissionName.CAN_FETCH_SUBJECTS);
            var canAssignSubjects = permissions.First(Permission => Permission.Name == PermissionName.CAN_ASSIGN_SUBJECT);
            var canFetchRole = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_ROLE);


            var attachPermissionCommand = new AttachPermissionsCommand()
            {
                PermissionIds = new List<Guid>() 
                { 
                    canRegisterStudent.Id,canAssignRole.Id,canFetchRoles.Id,
                    canRegisterNonAcadStaff.Id,canRegisterAcadStaffRole.Id,
                    canFetchStudents.Id,canFetchClasses.Id,canFetchSchools.Id,
                    canFetchSubjects.Id,canAssignSubjects.Id,canFetchRole.Id
                },
                RoleId = createRoleResponse.Data.Id
            };                                                        
                                                                       
            var attachPermissionResponse = await mediator.ExecuteCommandAsync<AttachPermissionsCommand, AttachPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(attachPermissionCommand);
        }

        private static async Task PersistStudentRole(IEnumerable<Permission>permissions, IMediator mediator)
        {
            var createRoleCommand = new CreateRoleCommand() { Title = "Student" };
            var createRoleResponse = await mediator.ExecuteCommandAsync<CreateRoleCommand, CreateRoleCommandHandler, UserManagementDbContext, CommandResponse>(createRoleCommand);

            var canFetchResults = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_STUDENT_RESULTS);
            var canFetchSubjects = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_STUDENT_SUBJECTS);
            var canFetchStudent = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_STUDENT);
            var canFetchRole = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_ROLE);

            var attachPermissionCommand = new AttachPermissionsCommand()
            {
                PermissionIds = new List<Guid>()
                {
                    canFetchResults.Id,
                    canFetchSubjects.Id,
                    canFetchStudent.Id,
                    canFetchRole.Id
                },
                
                RoleId = createRoleResponse.Data.Id
            };

            var attachPermissionResponse = await mediator.ExecuteCommandAsync<AttachPermissionsCommand, AttachPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(attachPermissionCommand);
        }

        private static async Task PersistNonAcademicStaffRole(IEnumerable<Permission> permissions, IMediator mediator)
        {
            var createRoleCommand = new CreateRoleCommand() { Title = "Non-Academic Staff" };
            var createRoleResponse = await mediator.ExecuteCommandAsync<CreateRoleCommand, CreateRoleCommandHandler, UserManagementDbContext, CommandResponse>(createRoleCommand);

            var canFetchStaff = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_NON_ACADEMICSTAFF);
            var canFetchRole = permissions.FirstOrDefault(permission => permission.Name == PermissionName.CAN_FETCH_ROLE);

            var attachPermissionCommand = new AttachPermissionsCommand()
            {
                PermissionIds = new List<Guid>()
                {
                    canFetchStaff.Id,canFetchRole.Id
                },

                RoleId = createRoleResponse.Data.Id
            };

            var attachPermissionResponse = await mediator.ExecuteCommandAsync<AttachPermissionsCommand, AttachPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(attachPermissionCommand);
        }

        private static async Task PersistAcademicStaffRole(IEnumerable<Permission> permissions, IMediator mediator)
        {
            var createRoleCommand = new CreateRoleCommand() { Title = "Academic Staff" };
            var createRoleResponse = await mediator.ExecuteCommandAsync<CreateRoleCommand, CreateRoleCommandHandler, UserManagementDbContext, CommandResponse>(createRoleCommand);

            var canFetchStudents = permissions.First(Permission => Permission.Name == PermissionName.CAN_FETCH_STUDENTS);
            var canFetchClasses = permissions.First(Permission => Permission.Name == PermissionName.CAN_FETCH_SCHOOLCLASSES);
            var canFetchSubjects = permissions.First(Permission => Permission.Name == PermissionName.CAN_FETCH_SUBJECTS);
            var canCreateResult = permissions.First(Permission => Permission.Name == PermissionName.CAN_CREATE_RESULT);
            var canFetchRole = permissions.First(permission => permission.Name == PermissionName.CAN_FETCH_ROLE);


            var attachPermissionCommand = new AttachPermissionsCommand()
            {
                PermissionIds = new List<Guid>()
                {
                    canFetchStudents.Id,canFetchClasses.Id,
                    canFetchSubjects.Id,canCreateResult.Id,canFetchRole.Id
                },
                RoleId = createRoleResponse.Data.Id
            };

            var attachPermissionResponse = await mediator.ExecuteCommandAsync<AttachPermissionsCommand, AttachPermissionsCommandHandler, UserManagementDbContext, CommandResponse>(attachPermissionCommand);
        }
    }
}
