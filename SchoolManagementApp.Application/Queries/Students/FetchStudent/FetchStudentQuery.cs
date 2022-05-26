using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudent
{
    public class FetchStudentQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(Id, "invalid student Id")
                .Result;
        }
        public Guid Id { get; set; }
    }
}
