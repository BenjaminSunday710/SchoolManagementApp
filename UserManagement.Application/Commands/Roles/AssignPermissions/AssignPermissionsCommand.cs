using Shared.Application.ArchitectureBuilder.Commands;
using System.Collections.Generic;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Roles.AssignPermissions
{
    public class AssignPermissionsCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(RoleId, $"{RoleId} is an invalid Id")
                .IsValidCollection(PermissionIds, $"{PermissionIds} is invalid permission Ids")
                .Result;
        }

        public int RoleId { get; set; }
        public IEnumerable<int> PermissionIds { get; set; }
    }
}
