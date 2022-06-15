using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Users.AuthenticateUser
{
    public class AuthenticateUserCommand:Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                 .IsValidEmail(Email, $"{Email} is invalid email")
                 .IsValidText(Password, $"{Password} is invalid password")
                 .IsValidText(RefreshToken, $"{RefreshToken} is invalid refresh token")
                 .Result;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string RefreshToken { get; set; }
    }
}
