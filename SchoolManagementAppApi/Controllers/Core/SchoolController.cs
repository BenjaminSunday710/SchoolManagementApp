using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.Schools.CreateSchool;
using SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaffs;
using SchoolManagementApp.Application.Queries.NonAcademicStaffs.FetchNonAcademicStaffs;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClasses;
using SchoolManagementApp.Application.Queries.Schools.FetchAllSchools;
using SchoolManagementApp.Application.Queries.Schools.FetchSchool;
using SchoolManagementApp.Application.Queries.Students.FetchStudents;
using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.NonAcademicStaffs;
using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Domain.Schools;
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
    [Route("schools")]
    public class SchoolController:ApiController
    {
        public SchoolController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Permission(PermissionName.CAN_CREATE_SCHOOL)]
        public async Task<IActionResult> CreateSchool(CreateSchoolCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateSchoolCommand, CreateSchoolCommandHandler, CoreDbContext, CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id",Name = "FetchSchool")]
        [Permission(PermissionName.CAN_FETCH_SCHOOL)]
        public async Task<IActionResult> FetchSchool(Guid id)
        {
            var query = new FetchSchoolQuery(id);
            var response = await Mediator.SendQueryAsync<School,FetchSchoolQuery, FetchSchoolQueryHandler, SchoolResponseDto>(query);
            return response.ResponseResult();
        }
        
        [HttpGet("{schoolId}/classes")]
        [Permission(PermissionName.CAN_FETCH_SCHOOLCLASSES)]
        public async Task<IActionResult> FetchSchoolClasses(Guid schoolId)
        {
            var query = new FetchSchoolClassesQuery(schoolId);
            var response = await Mediator.SendQueryAsync<SchoolClass, FetchSchoolClassesQuery, FetchSchoolClassesQueryHandler, List<SchoolClassResponseDto>>(query);
            return response.ResponseResult();
        }

        [HttpGet("{schoolId}/non-academic-staffs")]
        [Permission(PermissionName.CAN_FETCH_NON_ACADEMIC_STAFFS)]
        public async Task<IActionResult> FetchNonAcademicStaffs(Guid schoolId)
        {
            var query = new FetchNonAcademicStaffsQuery { SchoolId = schoolId };
            var response = await Mediator.SendQueryAsync<NonAcademicStaff, FetchNonAcademicStaffsQuery, FetchNonAcademicStaffsQueryHandler, List<NonAcademicStaffResponseDto>>(query);
            return response.ResponseResult();
        } 
        
        [HttpGet("{schoolId}/academic-staffs")]
        [Permission(PermissionName.CAN_FETCH_ACADEMICSTAFFS)]
        public async Task<IActionResult> FetchAcademicStaffs(Guid schoolId)
        {
            var query = new FetchAcademicStaffsQuery { SchoolId = schoolId };
            var response = await Mediator.SendQueryAsync<AcademicStaff, FetchAcademicStaffsQuery, FetchAcademicStaffsQueryHandler, List<AcademicStaffResponseDto>>(query);
            return response.ResponseResult();
        } 
        
        [HttpGet("{schoolId}/students")]
        [Permission(PermissionName.CAN_FETCH_STUDENTS)]
        public async Task<IActionResult> FetchStudents(Guid schoolId)
        {
            var query = new FetchStudentsQuery { SchoolId = schoolId };
            var response = await Mediator.SendQueryAsync<Student, FetchStudentsQuery, FetchStudentsQueryHandler, List<StudentResponseDto>>(query);
            return response.ResponseResult();
        } 
        
        [HttpGet()]
        [Permission(PermissionName.CAN_FETCH_SCHOOLS)]
        public async Task<IActionResult> FetchAllSchools()
        {
            var response = await Mediator.SendQueryAsync<School, FetchAllSchoolsQueryHandler, List<SchoolResponseDto>>();
            return response.ResponseResult();
        }
    }
}
