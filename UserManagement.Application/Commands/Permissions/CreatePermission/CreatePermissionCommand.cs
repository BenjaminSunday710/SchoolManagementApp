using Shared.Application.ArchitectureBuilder.Commands;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Commands.Permissions.CreatePermission
{
    public class CreatePermissionCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(Name, $"{Name} is invalid permission name")
                .Result;
        }

        public string Name { get; set; }
    }
}
