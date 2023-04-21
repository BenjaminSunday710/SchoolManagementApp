using Shared.Application.ArchitectureBuilder.Commands;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.Schools.CreateSchool
{
    public class CreateSchoolCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(Name, "Invalid school name")
                .IsValidInt(House_Number, "Invalid house number")
                .IsValidText(Street, "Invalid street")
                .IsValidText(City, "Invalid city")
                .IsValidText(StaffIdFormat, "Invalid employment id format")
                .IsValidText(StudentIdFormat, "Invalid student registration id format")
                .Result;
        }

        public string Name { get; set; }
        public string Website { get; set; }
        public int House_Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string StaffIdFormat { get; set; }
        public string StudentIdFormat { get; set; }
    }
}
