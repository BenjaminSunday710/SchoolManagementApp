using Shared.Application.ArchitectureBuilder.Commands;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.SchoolClasses.CreateSchoolClass
{
    public class CreateSchoolClassCommand:Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(Name, "Invalid class name")
                .IsValidInt(SchoolId, "Invalid school Id")
                .Result;
        }

        public int SchoolId { get; set; }
        public string Name { get; set; }
    }
}
