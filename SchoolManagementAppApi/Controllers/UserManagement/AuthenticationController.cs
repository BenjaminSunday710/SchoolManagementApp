using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> AuthenticateUser(AuthenticateUserCommand command)
        {
            var authenticatedUser = await Mediator.ExecuteCommandAsync<AuthenticateUserCommand, AuthenticateUserCommandHandler, UserManagementDbContext, AuthenticatedUserResponse>(command);

            if (authenticatedUser.NotSuccessful) return Unauthorized(authenticatedUser.Errors);

            var user = authenticatedUser.Data.User;

            //var userRoles = JsonConvert.SerializeObject(user.Roles);
            //var claims = new List<Claim>()
            //{
            //    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            //    new Claim(ClaimTypes.Email,user.Email),
            //    new Claim(ClaimTypes.Name,user.Email),
            //    new Claim(ClaimTypes.GivenName,user.FirstName),
            //    new Claim(ClaimTypes.Surname,user.LastName),
            //    new Claim("token",authenticatedUser.Data.Token)
            //};

            //var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
            //var principal = new ClaimsPrincipal(identity);

            //await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, principal);
            return Ok(new { UserId = user.Id, Token = authenticatedUser.Data.Token });
        }

        [HttpPost("log-out")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(JwtBearerDefaults.AuthenticationScheme);
            return Ok();
        }
    }
}
