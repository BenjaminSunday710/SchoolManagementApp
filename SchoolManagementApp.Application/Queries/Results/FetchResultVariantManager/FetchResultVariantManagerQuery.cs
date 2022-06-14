using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Results.FetchResultVariantManager
{
    public class FetchResultVariantManagerQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(Term.ToString(), $"{Term} is invalid school term")
                .IsValidText(Session, $"{Term} is invalid school session")
                .Result;
        }
        public string Session { get; set; }
        public Term Term { get; set; }
    }
}
