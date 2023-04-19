using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Permissions.DenyPermission
{
    public class DenyPermissionCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(RoleId, $"{RoleId} is invalid role id")
                .IsValidGuid(PermissionId, $"{PermissionId} is invalid permission id")
                .Result;
        }

        public Guid RoleId { get; set; }
        public Guid PermissionId { get; set; }
    }
}
