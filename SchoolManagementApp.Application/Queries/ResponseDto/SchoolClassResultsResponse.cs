using SchoolManagementApp.Domain.Results;
using System;

namespace SchoolManagementApp.Application.Queries.Results.FetchClassResults
{
    public class SchoolClassResultsResponse
    {
        public string Student { get; set; }
        public string SchoolClass { get; set; }
        public string Subject { get; set; }
        public string Session { get; set; }
        public Term Term { get; set; }
        public double ContinuousAssessment { get; set; }
        public double Examination { get; set; }
        public double Total { get; set; }
        public Grade Grade { get; set; }
        public Remark Remark { get; set; }
        public Guid Id { get; set; }
    }
}
