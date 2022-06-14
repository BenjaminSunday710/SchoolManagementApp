using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Roles.CreateRole
{
    public class CreateRoleCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(Title, $"{Title} is invalid role title")
                .Result;
        }

        public string Title { get; set; }
    }
}
