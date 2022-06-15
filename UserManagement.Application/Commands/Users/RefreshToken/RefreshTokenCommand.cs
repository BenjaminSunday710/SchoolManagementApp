using Shared.Application.ArchitectureBuilder.Commands;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Users.RefreshToken
{
    public class RefreshTokenCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(AccessToken, $"{AccessToken} is invalid access token")
                .IsValidText(RefreshToken, $"{RefreshToken} is invalid refresh token")
                .Result;
        }

        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
