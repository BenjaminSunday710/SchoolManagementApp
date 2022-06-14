using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Results.FetchClassResults
{
    public class FetchClassResultsQueryHandler : QueryHandler<Result, Guid, List<SchoolClassResultsResponse>, FetchClassResultsQuery>
    {
        public override async Task<ActionResult<List<SchoolClassResultsResponse>>> HandleAsync(FetchClassResultsQuery query, CancellationToken cancellationToken = default)
        {
            var subjectResults = await QueryContext.GetAllAsync();
            var results = subjectResults.Where(x => x.Subject.Id == query.SubjectId && x.ResultVariantManager.Id == query.ResultVariantManagerId);
            var response = new List<SchoolClassResultsResponse>();
            foreach (var result in results)
            {
                response.Add(new SchoolClassResultsResponse
                {
                    ContinuousAssessment = result.ContinuousAssessment,
                    Examination = result.Examination,
                    Total = result.Total,
                    Grade = result.Grade,
                    Remark = result.Remark,
                    SchoolClass = result.SchoolClass.Name,
                    Session = result.ResultVariantManager.Session,
                    Student = $"{result.Student.FirstName} {result.Student.LastName}",
                    Subject = result.Subject.Name,
                    Term = result.ResultVariantManager.Term,
                    Id = result.Id
                });
            }
            return OperationResult.Successful(response);
        }
    }
}
