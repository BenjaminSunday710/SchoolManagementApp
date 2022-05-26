using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Results.FetchResult
{
    public class FetchResultQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(Id, "invalid result Id")
                .Result;
        }
        public Guid Id { get; set; }
    }
}
