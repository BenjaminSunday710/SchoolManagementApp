using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Students.AssignSubjects;
using SchoolManagementApp.Application.Commands.Students.CreateStudent;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Students.FetchStudent;
using SchoolManagementApp.Application.Queries.Students.FetchStudentSubjects;
using SchoolManagementApp.Domain.Students;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers.Core
{
    [Route("students")]
    public class StudentController:ApiController
    {
        public StudentController(IMediator mediator) : base(mediator) { }

        [HttpPost()]
        public async Task<IActionResult> CreateStudent(CreateStudentCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateStudentCommand, CreateStudentCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchStudent(Guid id)
        {
            var query = new FetchStudentQuery { Id = id };
            var response = await Mediator.SendQueryAsync<Student,FetchStudentQuery, FetchStudentQueryHandler, StudentResponseDto>(query);
            return response.ResponseResult();
        }

        [HttpGet("{id}/subjects")]
        public async Task<IActionResult> FetchStudentSubjects(Guid id)
        {
            var query = new FetchStudentSubjectsQuery() { StudentId = id };
            var response = await Mediator.SendQueryAsync<Student, FetchStudentSubjectsQuery, FetchStudentSubjectsQueryHandler, List<SubjectResponseDto>>(query);
            return response.ResponseResult();
        }

        [HttpPut("assign-subjects")]
        public async Task<IActionResult> AssignSubjects(AssignStudentSubjectsCommand command)
        {
            var assignAction = await Mediator.ExecuteCommandAsync<AssignStudentSubjectsCommand, AssignStudentSubjectsCommandHandler, CoreDbContext, CommandResponse>(command);
            return assignAction.ResponseResult();
        }

    }
}
