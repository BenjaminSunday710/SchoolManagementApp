using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Users.AuthenticateUser;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.Security.TokenService;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.RefreshToken
{
    public class RefreshTokenCommandHandler : CommandHandler<RefreshTokenCommand, UserManagementDbContext, AuthenticatedUserResponse>
    {
        public override async Task<ActionResult<AuthenticatedUserResponse>> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken = default)
        {
            var tokenProvider = (ITokenProvider)ServiceProvider.GetService(typeof(ITokenProvider));
            var principal = tokenProvider.ProvidePrincipalFromExpiredToken(command.AccessToken);

            var user = await Context.UserRepository.GetByEmailAsync(principal.Identity.Name);
            if (user == null || user.TokenManager.RefreshToken != command.RefreshToken || user.TokenManager.RefreshTokenExpiryToken <=DateTime.UtcNow) 
                return OperationResult.Failed("Invalid user");

            var newAccessToken = tokenProvider.ProvideToken(user);
            var newRefreshToken = tokenProvider.ProvideRefreshToken();

            user.TokenManager.RefreshToken = newRefreshToken;
            user.TokenManager.RefreshTokenExpiryToken = DateTime.UtcNow.AddHours(24);

            await Context.UserRepository.UpdateAsync(user, user.Id);
            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("refresh token request failed");

            var response = new AuthenticatedUserResponse()
            {
                Token = newAccessToken,
                User = user,
                RefreshToken = newRefreshToken,
            };
            return OperationResult.Successful(response);
        }
    }
}
