using Microsoft.AspNetCore.Identity;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.Security.TokenService;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : CommandHandler<AuthenticateUserCommand, UserManagementDbContext, AuthenticatedUserResponse>
    {
        public override async Task<ActionResult<AuthenticatedUserResponse>> HandleAsync(AuthenticateUserCommand command, CancellationToken cancellationToken = default)
        {
            var user = await Context.UserRepository.GetByEmailAsync(command.Email);
            if (user == null) return OperationResult.Failed($"user with email-{command.Email} not found");

            if (!IsValidPassword(user, command.Password)) return OperationResult.Failed($"provided password is incorrect");

            var tokenProvider = (ITokenProvider)ServiceProvider.GetService(typeof(ITokenProvider));
            var token = tokenProvider.ProvideToken(user);

            var response = new AuthenticatedUserResponse() { Token = token, UserId = user.Id, Roles=user.Roles };
            return OperationResult.Successful(response);
        }

        private bool IsValidPassword(User user,string providedPassword)
        {
            var result= new PasswordHasher<User>().VerifyHashedPassword(user, user.Password, providedPassword);

            return (result == PasswordVerificationResult.Success) ? true : false;
        }

        private ITokenProvider tokenProvider;
    }
}
