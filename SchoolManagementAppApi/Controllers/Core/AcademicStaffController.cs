using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.AcademicStaffs.AssignSubjects;
using SchoolManagementApp.Application.Commands.AcademicStaffs.CreateAcademicStaff;
using SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaff;
using SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaffSubjects;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.AcademicStaffs;
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
    [Route("staff/academics")]
    public class AcademicStaffController:ApiController
    {
        public AcademicStaffController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Permission(PermissionName.CAN_CREATE_ACADEMICSTAFF)]
        public async Task<IActionResult> CreateAcademicStaff(CreateAcademicStaffCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateAcademicStaffCommand, CreateAcademicStaffCommandHandler,CoreDbContext,CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        [Permission(PermissionName.CAN_FETCH_ACADEMICSTAFF)]
        public async Task<IActionResult> FetchAcademicStaff(Guid id)
        {
            var query = new FetchAcademicStaffQuery() { Id = id };
            var response = await Mediator.SendQueryAsync<AcademicStaff,FetchAcademicStaffQuery, FetchAcademicStaffQueryHandler, AcademicStaffResponseDto>(query);
            return response.ResponseResult();
        }

        [HttpPut("assign-subjects")]
        [Permission(PermissionName.CAN_ASSIGN_SUBJECT)]
        public async Task<IActionResult> AssignSubjects(AssignAcademicStaffSubjectsCommand command)
        {
            var assignAction = await Mediator.ExecuteCommandAsync<AssignAcademicStaffSubjectsCommand, AssignAcademicStaffSubjectsCommandHandler, CoreDbContext, CommandResponse>(command);
            return assignAction.ResponseResult();
        }

        [HttpGet("{id}/subjects")]
        [Permission(PermissionName.CAN_FETCH_STAFF_SUBJECTS)]
        public async Task<IActionResult> FetchAcademicStaffSubjects(Guid id)
        {
            var query = new FetchAcademicStaffSubjectsQuery() { StaffId = id };
            var response = await Mediator.SendQueryAsync<AcademicStaff, FetchAcademicStaffSubjectsQuery, FetchAcademicStaffSubjectsQueryHandler, List<SubjectResponseDto>>(query);
            return response.ResponseResult();
        }
    }
}
