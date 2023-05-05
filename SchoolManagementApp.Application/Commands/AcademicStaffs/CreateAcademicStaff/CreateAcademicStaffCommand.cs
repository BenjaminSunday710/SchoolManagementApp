using SchoolManagementApp.Domain.SharedKernel.Persons;
using Shared.Application.ArchitectureBuilder.Commands;
using System;
using Utilities.Result.Util;
using Utilities.Validations;

namespace SchoolManagementApp.Application.Commands.AcademicStaffs.CreateAcademicStaff
{
    public class CreateAcademicStaffCommand : Command
    {
        protected override ActionResult Validate()
        {
            return new FluentValidator()
                .IsValidText(FirstName, "Invalid first name")
                .IsValidText(LastName, "Invalid last name")
                .IsValidInt(House_Number, "Invalid House_Number")
                .IsValidText(Street, "Invalid Street")
                .IsValidText(City, "Invalid City")
                .IsValidText(LG_of_Origin, "Invalid LG_of_Origin")
                .IsValidText(State_of_Origin, "Invalid State_of_Origin")
                .IsValidEmail(Email, "Invalid email")
                .IsValidGender(Gender, "Invalid gender")
                .IsValidText(PhoneNumber, "Invalid phone number")
                .IsValidGuid(SchoolId, "Invalid school Id")
                .Result;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int House_Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Designation { get; set; }
        public string LG_of_Origin { get; set; }
        public string State_of_Origin { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Guid SchoolId { get; set; }
    }
}
