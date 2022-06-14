using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudentsPerClass
{
    public class FetchStudentsPerClassQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(SchoolClassId, $"{SchoolClassId} is invalid school id")
                .Result;
        }
        public Guid SchoolClassId { get; set; }
    }
}
