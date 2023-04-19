using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.RemoveUser
{
    public class RemoveUserCommandHandler : CommandHandler<RemoveUserCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(RemoveUserCommand command, CancellationToken cancellationToken = default)
        {
            var user = await Context.UserRepository.GetByIdAsync(command.UserId);

            if (user == null) return OperationResult.Failed($"User with id:{command.UserId} not found");

            var response=new CommandResponse(user.Id);

            return OperationResult.Successful(response);
        }
    }
}
