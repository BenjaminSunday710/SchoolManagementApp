using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.AssignRole
{
    public class AssignRoleCommandHandler : CommandHandler<AssignRoleCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(AssignRoleCommand command, CancellationToken cancellationToken = default)
        {
            var user = await Context.UserRepository.GetByIdAsync(command.UserId);
            if (user == null) return OperationResult.Failed($"user with Id-{command.UserId} not found");

            var role = await Context.RoleRepository.GetByIdAsync(command.RoleId);
            if (role == null) return OperationResult.Failed($"role with Id-{command.RoleId} not found");

            user.AssignRole(role);
            await Context.UserRepository.UpdateAsync(user, user.Id);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to assign role");

            return OperationResult.Successful(new CommandResponse(user.Id));
        }
    }
}
