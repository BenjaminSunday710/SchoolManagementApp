using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Users.AuthenticateUser;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.RevokeToken
{
    public class RevokeTokenCommandHandler : CommandHandler<RevokeTokenCommand, UserManagementDbContext, AuthenticatedUserResponse>
    {
        public override async Task<ActionResult<AuthenticatedUserResponse>> HandleAsync(RevokeTokenCommand command, CancellationToken cancellationToken = default)
        {
            var user = await Context.UserRepository.GetByEmailAsync(command.Email);
            if (user == null) return OperationResult.Failed("Invalid user");
            user.SetRefreshTokenManager(null, DateTime.UtcNow);

            await Context.UserRepository.UpdateAsync(user, user.Id);
            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("revoke token request failed");

            var response = new AuthenticatedUserResponse()
            {
                Token = null,
                User = user,
                RefreshToken = null,
            };
            return OperationResult.Successful(response);
        }
    }
}
