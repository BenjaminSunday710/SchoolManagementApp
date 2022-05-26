using System;

namespace SchoolManagementApp.Application.Queries.ResponseDto
{
    public class SchoolResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int House_Number { get; set; }
    }
}
