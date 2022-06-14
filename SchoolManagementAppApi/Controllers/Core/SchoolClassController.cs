using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.SchoolClasses.CreateSchoolClass;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClass;
using SchoolManagementApp.Application.Queries.Students.FetchStudentsPerClass;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Students;
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
    [Route("schoolclasses")]
    public class SchoolClassController:ApiController
    {
        public SchoolClassController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Permission(PermissionName.CAN_CREATE_SCHOOLCLASS)]
        public async Task<IActionResult> CreateSchoolClass(CreateSchoolClassCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateSchoolClassCommand, CreateSchoolClassCommandHandler,CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        [Permission(PermissionName.CAN_FETCH_SCHOOLCLASS)]
        public async Task<IActionResult> FetchSchoolClass(Guid id)
        {
            var query = new FetchSchoolClassQuery(id);
            var response = await Mediator.SendQueryAsync<SchoolClass,FetchSchoolClassQuery, FetchSchoolClassQueryHandler, SchoolClassResponseDto>(query);
            return response.ResponseResult();
        }

        [HttpGet("{classId}/students")]
        [Permission(PermissionName.CAN_FETCH_CLASS_STUDENTS)]
        public async Task<IActionResult> FetchStudents(Guid classId)
        {
            var query = new FetchStudentsPerClassQuery { SchoolClassId = classId };
            var response = await Mediator.SendQueryAsync<Student, FetchStudentsPerClassQuery, FetchStudentsPerClassQueryHandler, List<StudentResponseDto>>(query);
            return response.ResponseResult();
        } 
        
    }
}
