using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Roles.AttachPermissions
{
    public class AttachPermissionsCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(RoleId, $"{RoleId} is an invalid Id")
                .IsValidCollection(PermissionIds, $"{PermissionIds} is invalid permission Ids")
                .Result;
        }

        public Guid RoleId { get; set; }
        public IEnumerable<Guid> PermissionIds { get; set; }
    }
}
