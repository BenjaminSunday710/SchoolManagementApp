using SchoolManagementApp.Application.Queries.Results.FetchClassResults;
using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Results.FetchSubjectResults
{
    public class FetchSubjectResultsQueryHandler : QueryHandler<Result, Guid, List<SchoolClassResultsResponse>, FetchSubjectResultsQuery>
    {
        public override async Task<ActionResult<List<SchoolClassResultsResponse>>> HandleAsync(FetchSubjectResultsQuery query, CancellationToken cancellationToken = default)
        {
            var subjectResults = await QueryContext.FindAsync(x => x.Subject.Id == query.SubjectId);
            var results = FilterResults(subjectResults,query.Session,query.Term);
            return OperationResult.Successful(results);
        }

        private List<SchoolClassResultsResponse> FilterResults(List<Result> subjectResults, string session, Term term)
        {
            var results = new List<SchoolClassResultsResponse>();
            foreach (var result in subjectResults)
            {
                if(result.ResultVariantManager.Session==session && result.ResultVariantManager.Term==term)
                {
                    results.Add(new SchoolClassResultsResponse
                    {
                        Id = result.Id,
                        Term = result.ResultVariantManager.Term,
                        Session = result.ResultVariantManager.Session,
                        ContinuousAssessment = result.ContinuousAssessment,
                        Examination = result.Examination,
                        Total = result.Total,
                        Grade = result.Grade,
                        Remark = result.Remark,
                        SchoolClass = result.SchoolClass.Name,
                        Student = $"{result.Student.FirstName} {result.Student.LastName}",
                        Subject = result.Subject.Name
                    });
                }
            }
            return results;
        }
    }
}
