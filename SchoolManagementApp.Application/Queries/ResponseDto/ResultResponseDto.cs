using SchoolManagementApp.Domain.Results;

namespace SchoolManagementApp.Application.Queries.ResponseDto
{
    public class ResultResponseDto
    {
        public int ClassId { get; set; }
        public string Session { get; set; }
        public Term Term { get; set; }
        public double ContinuousAssessment { get; set; }
        public double Examination { get; set; }
        public double Total { get; set; }
        public Grade Grade { get; set; }
        public Remark Remark { get; set; }
    }
}
