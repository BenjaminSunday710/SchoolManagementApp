using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Users.RemoveUser
{
    public class RemoveUserCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(UserId, $"{UserId} is invalid user id")
                .Result;
        }

        public Guid UserId { get; set; }
    }
}
