using System;

namespace SchoolManagementApp.Application.Commands.Students.CreateStudent
{
    public class CreateStudentResponse
    {
        public Guid Id { get; set; }
        public string RegistrationId { get; set; }
    }
}
