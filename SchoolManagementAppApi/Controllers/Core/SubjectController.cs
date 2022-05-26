using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Students.AssignSubjects;
using SchoolManagementApp.Application.Commands.Subjects.CreateSubject;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Students.FetchStudentSubjects;
using SchoolManagementApp.Application.Queries.Subjects.FetchSubject;
using SchoolManagementApp.Domain.Students;
using SchoolManagementApp.Domain.Subjects;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers.Core
{
    [Route("subjects")]
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
        public async Task<IActionResult> FetchStudent(Guid id)
        {
            var query = new FetchSubjectQuery { Id = id };
            var response = await Mediator.SendQueryAsync<Subject,FetchSubjectQuery, FetchSubjectQueryHandler,SubjectResponseDto>(query);
            return response.ResponseResult();
        }

    }
}
