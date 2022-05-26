using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.Students.AssignSubjects
{
    public class AssignStudentSubjectsCommand:Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(StudentId, "invalid staff Id")
                .IsValidCollection(SubjectsIds, "invalid subjects Ids")
                .Result;
        }

        public Guid StudentId { get; set; }
        public IEnumerable<Guid> SubjectsIds { get; set; }
    }
}
