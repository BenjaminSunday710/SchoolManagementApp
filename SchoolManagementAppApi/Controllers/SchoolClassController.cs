using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.SchoolClasses.CreateSchoolClass;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClass;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers
{
    [Route("api/schoolclasses")]
    public class SchoolClassController:ApiController
    {
        public SchoolClassController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateNonAcademicStaff(CreateSchoolClassCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateSchoolClassCommand, CreateSchoolClassCommandHandler,CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();

        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchNonAcademicStaff(int id)
        {
            var query = new FetchSchoolClassQuery(id);
            var response = await Mediator.SendQueryAsync<SchoolClass,FetchSchoolClassQuery, FetchSchoolClassQueryHandler, SchoolClassResponseDto>(query);
            return response.ResponseResult();

        }
    }
}
