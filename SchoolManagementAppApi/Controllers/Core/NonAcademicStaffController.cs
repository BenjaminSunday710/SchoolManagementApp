using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.NonAcademicStaffs.CreateNonAcademicStaff;
using SchoolManagementApp.Application.Queries.NonAcademicStaffs.FetchNonAcademicStaff;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.NonAcademicStaffs;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers.Core
{
    [Route("staff/nonAcademics")]
    public class NonAcademicStaffController:ApiController
    {
        public NonAcademicStaffController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateNonAcademicStaff(CreateNonAcademicStaffCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateNonAcademicStaffCommand, CreateNonAcademicStaffCommandHandler, CoreDbContext,CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchNonAcademicStaff(Guid id)
        {
            var query = new FetchNonAcademicStaffQuery() { Id = id };
            var response = await Mediator.SendQueryAsync<NonAcademicStaff,FetchNonAcademicStaffQuery, FetchNonAcademicStaffQueryHandler, NonAcademicStaffResponseDto>(query);
            return response.ResponseResult();

        }
    }
}
