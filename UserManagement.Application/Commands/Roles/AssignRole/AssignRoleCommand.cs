using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Roles.AssignRole
{
    public class AssignRoleCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(RoleId, $"{RoleId} is invalid role id")
                .IsValidGuid(UserId, $"{UserId} is invalid user id")
                .Result;
        }

        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
    }
}
