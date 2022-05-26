using Shared.Application.ArchitectureBuilder.Commands;
using System;
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
                .IsValidGuid(SchoolId, "Invalid school Id")
                .Result;
        }

        public Guid SchoolId { get; set; }
        public string Name { get; set; }
    }
}
