using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Results;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Results.FetchResult
{
    public class FetchResultQueryHandler : QueryHandler<Result, Guid, ResultResponseDto, FetchResultQuery>
    {
        public async override Task<ActionResult<ResultResponseDto>> HandleAsync(FetchResultQuery query, CancellationToken cancellationToken = default)
        {
            var result = await QueryContext.GetByIdAsync(query.Id);
            if (result == null) return OperationResult.Successful(new ResultResponseDto());
            var resultDto = new ResultResponseDto()
            {
                ClassId = result.SchoolClass.Id,
                ContinuousAssessment = result.ContinuousAssessment,
                Examination = result.Examination,
                Grade = result.Grade,
                Remark = result.Remark,
                Session = result.Session,
                Term = result.Term,
                Total = result.Total
            };
            return OperationResult.Successful(resultDto);
        }
    }
}
