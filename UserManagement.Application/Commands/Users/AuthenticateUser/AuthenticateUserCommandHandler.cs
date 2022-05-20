using Microsoft.AspNetCore.Identity;
using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.UserIdentity;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : CommandHandler<AuthenticateUserCommand, UserManagementDbContext, CommandResponse>
    {
        public override async Task<ActionResult<CommandResponse>> HandleAsync(AuthenticateUserCommand command, CancellationToken cancellationToken = default)
        {
            var user = await Context.UserRepository.GetByEmailAsync(command.Email);
            if (user == null) return OperationResult.Failed($"user with email-{command.Email} not found");

            if (!IsValidPassword(user, command.Password)) return OperationResult.Failed($"provided password-{command.Password} is incorrect");

            var token = JWTTokenGenerator.GenerateToken(user);
        }

        private bool IsValidPassword(User user,string providedPassword)
        {
            var result= new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, providedPassword);

            return (result == PasswordVerificationResult.Success) ? true : false;
        }
    }
}
