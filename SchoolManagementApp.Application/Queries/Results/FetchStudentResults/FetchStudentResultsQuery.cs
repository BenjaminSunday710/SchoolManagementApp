using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Results.FetchStudentResults
{
    public class FetchStudentResultsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(StudentId, $"{StudentId} is invalid student id")
                .IsValidGuid(ClassId, $"{ClassId} is invalid school class id")
                .IsValidText(Term.ToString(), $"{Term} is invalid school term")
                .Result;
        }
        public Guid StudentId { get; set; }
        public Term Term { get; set; }
        public Guid ClassId { get; set; }
    }
}
