using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudents
{
    public class FetchStudentsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(SchoolId, $"{SchoolId} is invalid school id")
                .Result;
        }
        public Guid SchoolId { get; set; }
    }
}
