using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.ResignRole
{
    public class ResignRoleCommandHandler : CommandHandler<ResignRoleCommand, UserManagementDbContext, bool>
    {
        public override async Task<ActionResult<bool>> HandleAsync(ResignRoleCommand command, CancellationToken cancellationToken = default)
        {
            var user = await Context.UserRepository.GetByIdAsync(command.UserId);
            if (user == null) return OperationResult.Failed($"User with id:{command.UserId} not found");

            var role = await Context.RoleRepository.GetByIdAsync(command.RoleId);
            if (role == null) return OperationResult.Failed($"Role with id:{command.RoleId} not found");

            user.ResignRole(role);

            await Context.UserRepository.UpdateAsync(user, command.UserId);

            var commitStatus = await Context.CommitAsync();

            return commitStatus.WasSuccessful ? OperationResult.Successful(true) : OperationResult.Failed("Unable to update user");
        }
    }
}
