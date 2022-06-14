using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Results.FetchStudentResults
{
    public class FetchStudentResultsQueryHandler : QueryHandler<Result, Guid, List<ResultResponseDto>, FetchStudentResultsQuery>
    {
        public override async Task<ActionResult<List<ResultResponseDto>>> HandleAsync(FetchStudentResultsQuery query, CancellationToken cancellationToken = default)
        {
            var studentResults = await QueryContext.FindAsync(x => x.Student.Id == query.StudentId);
            var results = studentResults.Where(x => x.SchoolClass.Id == query.ClassId && x.ResultVariantManager.Term == query.Term);
            var response = new List<ResultResponseDto>();
            foreach (var result in results)
            {
                response.Add(new ResultResponseDto
                {
                    SchoolClass = result.SchoolClass.Name,
                    Subject = result.Subject.Name,
                    ContinuousAssessment = result.ContinuousAssessment,
                    Examination = result.Examination,
                    Total = result.Total,
                    Grade = result.Grade,
                    Remark = result.Remark,
                    Term = result.ResultVariantManager.Term,
                    Session = result.ResultVariantManager.Session
                });
            }
            return OperationResult.Successful(response);
        }
    }
}
