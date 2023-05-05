using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace UserManagement.Application.Queries.Roles.FetchRoleByTitle
{
    public class FetchRoleByTitleQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(Title, $"{Title} is not a valid role title")
                .Result;
        }

        public string Title { get; set; }
    }
}
