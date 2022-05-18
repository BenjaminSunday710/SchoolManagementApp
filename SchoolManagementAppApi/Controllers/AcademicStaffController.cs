using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Application.Commands.AcademicStaffs.CreateAcademicStaff;
using SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaff;
using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Infrastructure.Context;
using SchoolManagementAppApi.ApplicationService;
using Shared.Application.ArchitectureBuilder.Commands;
using Shared.Application.Mediator;
using System.Threading.Tasks;

namespace SchoolManagementAppApi.Controllers
{
    [Route("api/staff/academics")]
    public class AcademicStaffController:ApiController
    {
        public AcademicStaffController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        public async Task<IActionResult> CreateAcademicStaff(CreateAcademicStaffCommand command)
        {
            var createAction = await Mediator.ExecuteCommandAsync<CreateAcademicStaffCommand, CreateAcademicStaffCommandHandler,CoreDbContext,CommandResponse>(command);
            return createAction.ResponseResult();
        }

        [HttpGet("id")]
        public async Task<IActionResult> FetchAcademicStaff(int id)
        {
            var query = new FetchAcademicStaffQuery() { Id = id };
            var response = await Mediator.SendQueryAsync<AcademicStaff,FetchAcademicStaffQuery, FetchAcademicStaffQueryHandler, AcademicStaffResponseDto>(query);
            return response.ResponseResult();

        }
    }
}
