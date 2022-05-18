using SchoolManagementApp.Domain.SharedKernel.Persons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementApp.Application.Queries.ResponseDto
{
    public class AcademicStaffResponseDto
    {
        public string FullName { get; set; }
        public string StateOfOrigin { get; set; }
        public string LG_Of_Origin { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House_Number { get; set; }
        public string Designation { get; set; }
        public int SchoolId { get; set; }
    }
}
