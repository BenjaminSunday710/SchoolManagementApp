using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Results.FetchResult
{
    public class FetchResultQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(Id, "invalid result Id")
                .Result;
        }
        public int Id { get; set; }
    }
}
