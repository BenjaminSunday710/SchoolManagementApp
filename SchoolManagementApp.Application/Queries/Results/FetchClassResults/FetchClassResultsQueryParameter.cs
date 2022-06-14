using SchoolManagementApp.Domain.Results;
using System;

namespace SchoolManagementApp.Application.Queries.Results.FetchClassResults
{
    public class FetchClassResultsQueryParameter
    {
        public Guid SubjectId { get; set; }
        public string Session { get; set; }
        public Term Term { get; set; }
    }
}
