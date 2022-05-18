using Shared.Application.ArchitectureBuilder.Queries;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Queries.NonAcademicStaffs.FetchNonAcademicStaff
{
    public class FetchNonAcademicStaffQuery : Query
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidInt(Id, "invalid staff Id")
                .Result;
        }
        public int Id { get; set; }
    }
}
