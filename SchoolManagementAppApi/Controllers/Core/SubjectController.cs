using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Subjects.CreateSubject;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.Subjects.FetchSubject;
using SchoolManagementApp.Application.Queries.Subjects.FetchSubjects;
using SchoolManagementApp.Domain.Subjects;
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
    [Route("subjects")]
    public class SubjectController:ApiController
    {
        public SubjectController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Permission(PermissionName.CAN_CREATE_SUBJECT)]
        public async Task<IActionResult> CreateSubject(CreateSubjectCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateSubjectCommand, CreateSubjectCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        [Permission(PermissionName.CAN_FETCH_SUBJECT)]
        public async Task<IActionResult> FetchSubject(Guid id)
        {
            var query = new FetchSubjectQuery { Id = id };
            var response = await Mediator.SendQueryAsync<Subject,FetchSubjectQuery, FetchSubjectQueryHandler,SubjectResponseDto>(query);
            return response.ResponseResult();
        }

        [HttpGet]
        [Permission(PermissionName.CAN_FETCH_SUBJECTS)]
        public async Task<IActionResult> FetchSubjects()
        {
            var response = await Mediator.SendQueryAsync<Subject, FetchSubjectsQueryHandler, List<SubjectResponseDto>>();
            return response.ResponseResult();
        }

    }
}
