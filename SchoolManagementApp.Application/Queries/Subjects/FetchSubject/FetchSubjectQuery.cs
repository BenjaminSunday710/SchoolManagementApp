using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.Subjects.FetchSubject
{
    public class FetchSubjectQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(Id, "invalid subject Id")
                .Result;
        }
        public Guid Id { get; set; }
    }
}
