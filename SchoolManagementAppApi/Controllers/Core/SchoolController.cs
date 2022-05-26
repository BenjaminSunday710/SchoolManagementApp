using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Schools.CreateSchool;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Schools.FetchAllSchools;
using SchoolManagementApp.Application.Queries.Schools.FetchSchool;
using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementAppApi.Controllers.Core
{
    [Route("schools")]
    public class SchoolController:ApiController
    {
        public SchoolController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateSchool(CreateSchoolCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateSchoolCommand, CreateSchoolCommandHandler, CoreDbContext, CommandResponse>(command);
            
            return createAction.ResponseResult();
        }

        [HttpGet("id",Name = "FetchSchool")]
        public async Task<IActionResult> FetchSchool(Guid id)
        {
            var query = new FetchSchoolQuery(id);
            var response = await Mediator.SendQueryAsync<School,FetchSchoolQuery, FetchSchoolQueryHandler, SchoolResponseDto>(query);

            return response.ResponseResult();

        } 
        
        [HttpGet()]
        public async Task<IActionResult> FetchAllSchools()
        {
            var response = await Mediator.SendQueryAsync<School, FetchAllSchoolsQueryHandler, List<SchoolResponseDto>>();
            return response.ResponseResult();
        }
    }
}
