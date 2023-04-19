using Microsoft.AspNetCore.Identity;
using Shared.Application.ArchitectureBuilder.Commands;
using System;
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

            //var tokenProvider = (ITokenProvider)ServiceProvider.GetService(typeof(ITokenProvider));
            var token = TokenProvider.ProvideToken(user);
            var refreshToken = TokenProvider.ProvideRefreshToken();

            user.SetRefreshTokenManager(refreshToken, DateTime.UtcNow.AddHours(24));

            await Context.UserRepository.UpdateAsync(user, user.Id);
            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("user authentication failed");

            var response = new AuthenticatedUserResponse()
            {
                Token = token,
                User = new UserDto
                {
                    UserId=user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                },
                RefreshToken = refreshToken,
            };
            return OperationResult.Successful(response);
        }

        private bool IsValidPassword(User user,string providedPassword)
        {
            var result= new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, providedPassword);

            return (result == PasswordVerificationResult.Success) ? true : false;
        }

        public ITokenProvider TokenProvider { get; set; }
    }
}
