using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Results.FetchSubjectResults
{
    public class FetchSubjectResultsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(SubjectId, $"{SubjectId} is invalid subject id")
                .IsValidText(Session, $"{Session} is invalid school session")
                .IsValidText(Term.ToString(), $"{Term} is invalid school term")
                .Result;
        }

        public Guid SubjectId { get; set; }
        public string Session { get; set; }
        public Term Term { get; set; }
    }
}
