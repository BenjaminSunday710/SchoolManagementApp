using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Schools.CreateSchool;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Schools.FetchSchool;
using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers
{
    [Route("api/schools")]
    public class SchoolController:ApiController
    {
        public SchoolController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateNonAcademicStaff(CreateSchoolCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateSchoolCommand, CreateSchoolCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();

        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchNonAcademicStaff(int id)
        {
            var query = new FetchSchoolQuery(id);
            var response = await Mediator.SendQueryAsync<School,FetchSchoolQuery, FetchSchoolQueryHandler, SchoolResponseDto>(query);
            return response.ResponseResult();

        }
    }
}
