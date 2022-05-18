using Shared.Application.ArchitectureBuilder.Commands;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Roles;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Roles.AssignPermissions
{
    public class AssignPermissionsCommandHandler : CommandHandler<AssignPermissionsCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(AssignPermissionsCommand command, CancellationToken cancellationToken = default)
        {
            var role = await Context.RoleRepository.GetByIdAsync(command.RoleId);
            if (role == null) return OperationResult.Failed($"role with id-{command.RoleId} does not exist");
            response = new CommandResponse(role.Id);
            await AssignPermissions(role, command.PermissionIds);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) OperationResult.Failed("unable to assign permissions");

            return OperationResult.Successful(response);
        }

        private async Task AssignPermissions(Role role, IEnumerable<int> permissionIds)
        {
            foreach (var permissionId in permissionIds)
            {
                var permission = await Context.PermissionRepository.GetByIdAsync(permissionId);
                if (permission == null) response.NotifyInvalidItems($"{permissionId} is invalid permission Id");
                else
                {
                    var rolePermission = new RolePermission(role, permission);
                    await Context.RolePermissionRepository.AddAsync(rolePermission);
                }
            }
        }

        private CommandResponse response;
    }
}
