using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudent
{
    public class FetchStudentQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(Id, "invalid student Id")
                .Result;
        }
        public int Id { get; set; }
    }
}
