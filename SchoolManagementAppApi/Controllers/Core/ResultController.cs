using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Results.CreateResult;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Results.FetchResult;
using SchoolManagementApp.Domain.Results;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers.Core
{
    [Route("results")]
    public class ResultController:ApiController
    {
        public ResultController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateResultCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateResultCommand, CreateResultCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();

        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchStudent(Guid id)
        {
            var query = new FetchResultQuery { Id = id };
            var response = await Mediator.SendQueryAsync<Result,FetchResultQuery, FetchResultQueryHandler,ResultResponseDto>(query);
            return response.ResponseResult();

        }
    }
}
