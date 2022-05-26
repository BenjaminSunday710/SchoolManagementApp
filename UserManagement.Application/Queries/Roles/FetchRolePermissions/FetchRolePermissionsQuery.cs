using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Queries.Roles.FetchRolePermissions
{
    public class FetchRolePermissionsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(RoleId, $"{RoleId} is invalid role id")
                .Result;
        }
        public Guid RoleId { get; set; }
    }
}
