namespace SchoolManagementApp.Application.Queries.ResponseDto
{
    public class SubjectResponseDto
    {
        public SubjectResponseDto() { }
        public SubjectResponseDto(string name, string className)
        {
            Name = name;
            ClassName = className;
        }

        public string Name { get; set; }
        public string ClassName { get; set; }
    }
}
