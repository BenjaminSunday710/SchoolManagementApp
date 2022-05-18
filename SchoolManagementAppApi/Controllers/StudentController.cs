using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Students.CreateStudent;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Students.FetchStudent;
using SchoolManagementApp.Domain.Students;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers
{
    [Route("api/students")]
    public class StudentController:ApiController
    {
        public StudentController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateStudent(CreateStudentCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateStudentCommand, CreateStudentCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();

        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchStudent(int id)
        {
            var query = new FetchStudentQuery { Id = id };
            var response = await Mediator.SendQueryAsync<Student,FetchStudentQuery, FetchStudentQueryHandler, StudentResponseDto>(query);
            return response.ResponseResult();

        }
    }
}
