using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Roles;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Permissions.AttachPermissions
{
    public class AttachPermissionsCommandHandler : CommandHandler<AttachPermissionsCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(AttachPermissionsCommand command, CancellationToken cancellationToken = default)
        {
            var role = await Context.RoleRepository.GetByIdAsync(command.RoleId);
            if (role == null) return OperationResult.Failed($"role with id-{command.RoleId} does not exist");
            response = new CommandResponse(role.Id);
            await AttachPermissions(role, command.PermissionIds);
            await Context.RoleRepository.UpdateAsync(role, role.Id);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) OperationResult.Failed("unable to assign permissions");

            return OperationResult.Successful(response);
        }

        private async Task AttachPermissions(Role role, IEnumerable<Guid> permissionIds)
        {
            foreach (var permissionId in permissionIds)
            {
                var permission = await Context.PermissionRepository.GetByIdAsync(permissionId);
                if (permission == null) response.NotifyInvalidItems($"{permissionId} is invalid permission Id");
                else role.AllowPermission(permission);
            }
        }

        private CommandResponse response;
    }
}
