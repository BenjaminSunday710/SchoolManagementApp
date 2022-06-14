using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.NonAcademicStaffs.FetchNonAcademicStaffs
{
    public class FetchNonAcademicStaffsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(SchoolId, $"{SchoolId} is invalid id")
                .Result;
        }
        public Guid SchoolId { get; set; }
    }
}
