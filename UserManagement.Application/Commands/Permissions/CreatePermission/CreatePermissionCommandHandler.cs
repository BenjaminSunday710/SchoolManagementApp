using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Permissions;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Permissions.CreatePermission
{
    public class CreatePermissionCommandHandler : CommandHandler<CreatePermissionCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(CreatePermissionCommand command, CancellationToken cancellationToken = default)
        {
            var exists = await Context.PermissionRepository.ExistsAsync(x => x.Name == command.Name);
            if (exists) return OperationResult.Failed($"permission with name-{command.Name} already exist");

            var permission = new Permission(command.Name);
            await Context.PermissionRepository.AddAsync(permission);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to save permission");

            return OperationResult.Successful(new CommandResponse(permission.Id));
        }
    }
}
