using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Schools.FetchSchool
{
    public class FetchSchoolQuery : Query
    {
        public FetchSchoolQuery(Guid id)
        {
            Id = id;
        }

        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(Id, $"invalid school Id")
                .Result;
        }

        public Guid Id { get; set; }
    }
}
