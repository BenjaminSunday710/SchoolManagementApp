using System;

namespace SchoolManagementApp.Application.Queries.ResponseDto
{
    public class SchoolClassResponseDto
    {
        public string Name { get; set; }
        public Guid SchoolId { get; set; }
        public Guid ClassTeacherId { get; set; }
    }
}
