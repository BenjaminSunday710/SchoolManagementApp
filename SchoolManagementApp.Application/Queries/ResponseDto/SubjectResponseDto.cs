using System;

namespace SchoolManagementApp.Application.Queries.ResponseDto
{
    public class SubjectResponseDto
    {
        public SubjectResponseDto() { }
        public SubjectResponseDto(string name, string className, Guid id)
        {
            Name = name;
            ClassName = className;
            Id = id;
        }

        public string Name { get; set; }
        public string ClassName { get; set; }
        public Guid Id { get; set; }
    }
}
