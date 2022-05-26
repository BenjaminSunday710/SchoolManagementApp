using SchoolManagementApp.Domain.NonAcademicStaffs;
using SchoolManagementApp.Domain.SharedKernel.Persons;
using System;

namespace SchoolManagementApp.Application.Queries.ResponseDto
{
    public class NonAcademicStaffResponseDto
    {
        public string FullName { get; set; }
        public string StateOfOrigin { get; set; }
        public string LG_Of_Origin { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House_Number { get; set; }
        public string Designation { get; set; }
        public Unit StaffUnit { get; set; }
        public Guid SchoolId { get; set; }
    }
}
