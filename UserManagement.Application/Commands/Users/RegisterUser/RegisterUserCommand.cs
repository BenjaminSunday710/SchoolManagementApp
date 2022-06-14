using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Users.RegisterUser
{
    public class RegisterUserCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidEmail(Email, $"{Email} is invalid email")
                .IsValidText(Password, $"{Password} is invalid password")
                .IsValidText(FirstName, $"{FirstName} is invalid first name")
                .IsValidText(LastName, $"{LastName} is invalid last name")
                .Result;
        }

        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
