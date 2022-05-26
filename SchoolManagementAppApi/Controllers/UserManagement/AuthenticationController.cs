using Microsoft.AspNetCore.Mvc;
using Shared.Application.Mediator;
using System.Threading.Tasks;
using UserManagement.Application.Commands.Users.AuthenticateUser;
using UserManagement.Infrastructure.Context;

namespace SchoolManagementAppApi.Controllers.UserManagement
{
    [Route("authentications")]
    public class AuthenticationController:ApiController
    {
        public AuthenticationController(IMediator mediator) : base(mediator) { }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUserCommand command)
        {
            var authenticatedUser = await Mediator.ExecuteCommandAsync<AuthenticateUserCommand, AuthenticateUserCommandHandler, UserManagementDbContext, AuthenticatedUserResponse>(command);
            return Ok(authenticatedUser);
        }
    }
}
