using Microsoft.AspNetCore.Identity;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommandHandler : CommandHandler<RegisterUserCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(RegisterUserCommand command, CancellationToken cancellationToken = default)
        {
            var exists = await Context.UserRepository.ExistsAsync(user => user.Email == command.Email);
            if (exists) return OperationResult.Failed($"user with email-{command.Email} already exist");

            var hashedPassword = new PasswordHasher<User>().HashPassword(null, command.Password);
            var user = new User(command.FirstName, command.LastName, command.Email, hashedPassword);

            await Context.UserRepository.AddAsync(user);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to register user");

            return OperationResult.Successful(new CommandResponse(user.Id));
        }
    }
}
