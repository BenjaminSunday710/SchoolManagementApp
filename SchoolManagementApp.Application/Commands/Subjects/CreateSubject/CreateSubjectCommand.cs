using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.Subjects.CreateSubject
{
    public class CreateSubjectCommand:Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(SchoolClassId, "invalid school class Id")
                .IsValidText(Name, "invalid subject name")
                .Result;
        }

        public Guid SchoolClassId { get; set; }
        public string Name { get; set; }
    }
}
