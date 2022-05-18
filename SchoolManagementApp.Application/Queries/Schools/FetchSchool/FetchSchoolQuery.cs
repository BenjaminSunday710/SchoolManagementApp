using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Schools.FetchSchool
{
    public class FetchSchoolQuery : Query
    {
        public FetchSchoolQuery(int id)
        {
            Id = id;
        }

        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(Id, $"invalid school Id")
                .Result;
        }

        public int Id { get; set; }
    }
}
