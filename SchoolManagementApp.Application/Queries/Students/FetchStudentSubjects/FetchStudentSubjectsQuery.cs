using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudentSubjects
{
    public class FetchStudentSubjectsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(StudentId, $"{StudentId} is invalid student id")
                .Result;
        }
        public Guid StudentId { get; set; }
    }
}
