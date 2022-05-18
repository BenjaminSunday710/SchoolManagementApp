using Shared.Application.ArchitectureBuilder.Commands;
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
                .IsValidInt(StaffId, "invalid staff Id")
                .IsValidCollection(SubjectsIds, "invalid subjects Ids")
                .Result;
        }

        public int StaffId { get; set; }
        public IEnumerable<int> SubjectsIds { get; set; }
    }
}
