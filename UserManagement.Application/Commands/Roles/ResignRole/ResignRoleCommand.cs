using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Roles.ResignRole
{
    public class ResignRoleCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(UserId, $"{UserId} is invalid user id")
                .IsValidGuid(RoleId, $"{RoleId} is invalid role id")
                .Result;
        }

        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
