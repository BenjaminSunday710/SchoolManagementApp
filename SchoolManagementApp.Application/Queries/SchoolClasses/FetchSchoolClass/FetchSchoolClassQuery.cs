using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClass
{
    public class FetchSchoolClassQuery : Query
    {
        public FetchSchoolClassQuery(Guid id)
        {
            Id = id;
        }

        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(Id, "invalid school class Id")
                .Result;
        }

        public Guid Id { get; set; }
    }
}
