using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Results.FetchClassResults
{
    public class FetchClassResultsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(SubjectId, $"{SubjectId} is invalid subject Id")
                .IsValidGuid(ResultVariantManagerId, $"{ResultVariantManagerId} is invalid ResultVariantManager Id")
                .Result;
        }

        public Guid SubjectId { get; set; }
        public Guid ResultVariantManagerId { get; set; }
    }
}
