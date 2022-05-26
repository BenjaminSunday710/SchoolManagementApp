using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaffSubjects
{
    public class FetchAcademicStaffSubjectsQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(StaffId, $"{StaffId} is invalid academic staff id")
                .Result;
        }
        public Guid  StaffId { get; set; }
    }
}
