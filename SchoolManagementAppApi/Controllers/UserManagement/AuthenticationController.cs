using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Users.AuthenticateUser;
using UserManagement.Application.Commands.Users.RefreshToken;
using UserManagement.Application.Commands.Users.RegisterUser;
using UserManagement.Application.Commands.Users.RevokeToken;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("authentications")]
    public class AuthenticationController:ApiController
    {
        public AuthenticationController(IMediator mediator) : base(mediator) { }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUserCommand command)
        {
            var authenticatedUser = await Mediator.ExecuteCommandAsync<AuthenticateUserCommand, AuthenticateUserCommandHandler, UserManagementDbContext, AuthenticatedUserResponse>(command);

            if (authenticatedUser.NotSuccessful) return Unauthorized(authenticatedUser.Errors);

            return Ok(authenticatedUser);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(RegisterUserCommand command)
        {
            var registerUser = await Mediator.ExecuteCommandAsync<RegisterUserCommand, RegisterUserCommandHandler, UserManagementDbContext, CommandResponse>(command);

            return Ok(registerUser);
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand command)
        {
            var refreshedToken = await Mediator.ExecuteCommandAsync<RefreshTokenCommand, RefreshTokenCommandHandler, UserManagementDbContext, AuthenticatedUserResponse>(command);

            if (refreshedToken.NotSuccessful) return Unauthorized(refreshedToken.Errors);

            return Ok(refreshedToken.Data);
        } 
        
        [HttpPost("revoke-token")]
        [Authorize]
        public async Task<IActionResult> RevokeToken(RevokeTokenCommand command)
        {
            var refreshedToken = await Mediator.ExecuteCommandAsync<RevokeTokenCommand, RevokeTokenCommandHandler, UserManagementDbContext, AuthenticatedUserResponse>(command);

            if (refreshedToken.NotSuccessful) return Unauthorized(refreshedToken.Errors);

            return NoContent();
        }

        [HttpPost("log-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
    