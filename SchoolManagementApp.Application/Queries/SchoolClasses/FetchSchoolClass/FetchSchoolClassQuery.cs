using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClass
{
    public class FetchSchoolClassQuery : Query
    {
        public FetchSchoolClassQuery(int id)
        {
            Id = id;
        }

        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(Id, "invalid school class Id")
                .Result;
        }

        public int Id { get; set; }
    }
}
