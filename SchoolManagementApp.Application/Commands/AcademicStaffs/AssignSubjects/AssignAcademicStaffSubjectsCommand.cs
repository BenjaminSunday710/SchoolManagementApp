using Shared.Application.ArchitectureBuilder.Commands;
using System;
using System.Collections.Generic;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.AcademicStaffs.AssignSubjects
{
    public class AssignAcademicStaffSubjectsCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidGuid(StaffId, "invalid staff Id")
                .IsValidCollection(SubjectsIds, "invalid subjects Ids")
                .Result;
        }

        public Guid StaffId { get; set; }
        public IEnumerable<Guid> SubjectsIds { get; set; }
    }
}
