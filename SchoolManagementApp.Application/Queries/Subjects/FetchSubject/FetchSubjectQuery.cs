using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Subjects.FetchSubject
{
    public class FetchSubjectQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(Id, "invalid subject Id")
                .Result;
        }
        public int Id { get; set; }
    }
}
