using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClasses
{
    public class FetchSchoolClassesQuery : Query
    {
        public FetchSchoolClassesQuery(Guid id)
        {
            SchoolId = id;
        }

        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(SchoolId, $"{SchoolId} is invalid school id")
                .Result;
        }

        public Guid SchoolId { get; set; }
    }
}
