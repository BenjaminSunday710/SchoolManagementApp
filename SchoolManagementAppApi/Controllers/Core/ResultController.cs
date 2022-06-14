using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Results.CreateResult;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Results.FetchClassResults;
using SchoolManagementApp.Application.Queries.Results.FetchResult;
using SchoolManagementApp.Application.Queries.Results.FetchResultVariantManager;
using SchoolManagementApp.Application.Queries.Results.FetchStudentResults;
using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using SchoolManagementAppApi.ApplicationService.Authorizations;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers.Core
{
    [Route("results")]
    public class ResultController:ApiController
    {
        public ResultController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Permission(PermissionName.CAN_CREATE_RESULT)]
        public async Task<IActionResult> CreateResult(CreateResultCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateResultCommand, CreateResultCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        [Permission(PermissionName.CAN_FETCH_RESULT)]
        public async Task<IActionResult> FetchResult(Guid id)
        {
            var query = new FetchResultQuery { Id = id };
            var response = await Mediator.SendQueryAsync<Result,FetchResultQuery, FetchResultQueryHandler,ResultResponseDto>(query);
            return response.ResponseResult();
        }

        [HttpGet("class-results")]
        [Permission(PermissionName.CAN_FETCH_CLASS_RESULTS)]
        public async Task<IActionResult> FetchClassResult([FromQuery] FetchClassResultsQueryParameter parameter)
        {
            var resultVariantQuery = new FetchResultVariantManagerQuery
            {
                Term = parameter.Term,
                Session = parameter.Session
            };
            var resultVariantResponse = await Mediator.SendQueryAsync<ResultVariantManager, FetchResultVariantManagerQuery, FetchResultVariantManagerQueryHandler, ResultVariantManagerResponse>(resultVariantQuery);

            var resultQuery = new FetchClassResultsQuery
            {
                ResultVariantManagerId = resultVariantResponse.Data.Id,
                SubjectId = parameter.SubjectId
            };

            var response = await Mediator.SendQueryAsync<Result, FetchClassResultsQuery, FetchClassResultsQueryHandler, List<SchoolClassResultsResponse>>(resultQuery);

            return response.ResponseResult();
        }

        [HttpGet("student-results")]
        [Permission(PermissionName.CAN_FETCH_STUDENT_RESULTS)]
        public async Task<IActionResult> FetchStudentResults([FromQuery]FetchStudentResultsQuery query)
        {
            var response = await Mediator.SendQueryAsync<Result, FetchStudentResultsQuery, FetchStudentResultsQueryHandler, List<ResultResponseDto>>(query);
            return response.ResponseResult();
        }
        
        [HttpGet("subject-results")]
        [Permission(PermissionName.CAN_FETCH_SUBJECT_RESULTS)]
        public async Task<IActionResult> FetchSubjectResults([FromQuery]FetchStudentResultsQuery query)
        {
            var response = await Mediator.SendQueryAsync<Result, FetchStudentResultsQuery, FetchStudentResultsQueryHandler, List<ResultResponseDto>>(query);
            return response.ResponseResult();
        }
    }
}
