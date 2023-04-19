using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Permissions.DenyPermission
{
    public class DenyPermissionCommandHandler : CommandHandler<DenyPermissionCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(DenyPermissionCommand command, CancellationToken cancellationToken = default)
        {
            var role = await Context.RoleRepository.GetByIdAsync(command.RoleId);

            if (role == null) return OperationResult.Failed($"Role with id:{command.RoleId} not found");

            var permission = await Context.PermissionRepository.GetByIdAsync(command.PermissionId);

            role.DenyPermission(permission);
            var response = new CommandResponse(role.Id);

            await Context.RoleRepository.UpdateAsync(role,command.RoleId);
            var commitStatus = await Context.CommitAsync();

            return commitStatus.WasSuccessful ? OperationResult.Successful(response) : OperationResult.Failed("Unable to deny permission");
        }
    }
}
