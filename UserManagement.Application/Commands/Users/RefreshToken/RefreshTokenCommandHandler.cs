using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Users.AuthenticateUser;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.Security.TokenService;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Users.RefreshToken
{
    public class RefreshTokenCommandHandler : CommandHandler<RefreshTokenCommand, UserManagementDbContext, AuthenticatedUserResponse>
    {
        private ITokenProvider tokenProvider;

        public RefreshTokenCommandHandler(ITokenProvider provider)
        {
            tokenProvider = provider;
        }

        public override async Task<ActionResult<AuthenticatedUserResponse>> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken = default)
        {
            //var tokenProvider = (ITokenProvider)ServiceProvider.GetService(typeof(ITokenProvider));
            var principal = tokenProvider.ProvidePrincipalFromExpiredToken(command.AccessToken);
            var userEmail = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await Context.UserRepository.GetByEmailAsync(userEmail);
            if (user == null || user.TokenManager.RefreshToken != command.RefreshToken || user.TokenManager.RefreshTokenExpiryTime <=DateTime.UtcNow) 
                return OperationResult.Failed("Invalid user");

            var newAccessToken = tokenProvider.ProvideToken(user);
            var newRefreshToken = tokenProvider.ProvideRefreshToken();

            user.SetRefreshTokenManager(newRefreshToken, DateTime.UtcNow.AddHours(24));

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
