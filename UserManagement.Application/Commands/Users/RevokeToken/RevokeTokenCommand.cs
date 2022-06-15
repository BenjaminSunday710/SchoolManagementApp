using Shared.Application.ArchitectureBuilder.Commands;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Users.RevokeToken
{
    public class RevokeTokenCommand:Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(Email, $"{Email} is invalid user email")
                .Result;
        }

        public string Email { get; set; }
    }
}
