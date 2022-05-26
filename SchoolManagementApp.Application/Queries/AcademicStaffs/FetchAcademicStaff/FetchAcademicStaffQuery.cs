using Shared.Application.ArchitectureBuilder.Queries;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaff
{
    public class FetchAcademicStaffQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(Id, "invalid staff Id")
                .Result;
        }

        public Guid Id { get; set; }
    }
}
