using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Queries.Users.FetchUserRoles
{
    public class FetchUserRolesQuery : Query
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
