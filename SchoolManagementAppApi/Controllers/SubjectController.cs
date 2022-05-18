using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Subjects.CreateSubject;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Subjects.FetchSubject;
using SchoolManagementApp.Domain.Subjects;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers
{
    [Route("api/subjects")]
    public class SubjectController:ApiController
    {
        public SubjectController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateSubject(CreateSubjectCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateSubjectCommand, CreateSubjectCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchStudent(int id)
        {
            var query = new FetchSubjectQuery { Id = id };
            var response = await Mediator.SendQueryAsync<Subject,FetchSubjectQuery, FetchSubjectQueryHandler,SubjectResponseDto>(query);
            return response.ResponseResult();
        }
    }
}
