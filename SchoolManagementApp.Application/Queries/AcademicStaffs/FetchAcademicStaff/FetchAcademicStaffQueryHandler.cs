using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.AcademicStaffs;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaff
{
    public class FetchAcademicStaffQueryHandler : QueryHandler<AcademicStaff, Guid, AcademicStaffResponseDto, FetchAcademicStaffQuery>
    {
        public async override Task<ActionResult<AcademicStaffResponseDto>> HandleAsync(FetchAcademicStaffQuery query, CancellationToken cancellationToken = default)
        {
            var staff = await QueryContext.GetByIdAsync(query.Id);
            if (staff == null) return OperationResult.Successful(new AcademicStaffResponseDto());
            
            var staffDto = new AcademicStaffResponseDto()
            {
                FullName = $"{staff.FirstName} {staff.LastName}",
                House_Number = staff.Address.House_Number,
                Street = staff.Address.Street,
                City = staff.Address.City,
                LG_Of_Origin = staff.LG_Of_Origin,
                StateOfOrigin = staff.StateOfOrigin,
                Age = staff.GetAge(),
                Designation = staff.Designation,
                Gender = staff.Gender,
                PhoneNumber = staff.PhoneNumber,
                SchoolId=staff.School.Id
            };

            return OperationResult.Successful(staffDto);
        }
    }
}
