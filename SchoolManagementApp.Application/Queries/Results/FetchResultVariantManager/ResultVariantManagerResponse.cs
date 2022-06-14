using SchoolManagementApp.Domain.Results;
using System;

namespace SchoolManagementApp.Application.Queries.Results.FetchResultVariantManager
{
    public class ResultVariantManagerResponse
    {
        public string Session { get; set; }
        public Term Term { get; set; }
        public Guid Id { get; set; }
    }
}
