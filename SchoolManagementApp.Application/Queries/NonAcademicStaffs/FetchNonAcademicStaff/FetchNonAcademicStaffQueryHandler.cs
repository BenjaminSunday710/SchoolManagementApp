using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.NonAcademicStaffs;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.NonAcademicStaffs.FetchNonAcademicStaff
{
    public class FetchNonAcademicStaffQueryHandler : QueryHandler<NonAcademicStaff, Guid, NonAcademicStaffResponseDto, FetchNonAcademicStaffQuery>
    {
        public async override Task<ActionResult<NonAcademicStaffResponseDto>> HandleAsync(FetchNonAcademicStaffQuery query, CancellationToken cancellationToken = default)
        {
            var staff = await QueryContext.GetByIdAsync(query.Id);
            if (staff == null) return OperationResult.Successful(new NonAcademicStaffResponseDto());
            var staffDto = new NonAcademicStaffResponseDto()
            {
                Age = staff.GetAge(),
                City = staff.Address.City,
                Designation = staff.Designation,
                FullName = $"{staff.FirstName} {staff.LastName}",
                Gender = staff.Gender,
                House_Number = staff.Address.House_Number,
                LG_Of_Origin = staff.LG_Of_Origin,
                PhoneNumber = staff.PhoneNumber,
                SchoolId = staff.School.Id,
                StaffUnit = staff.Unit,
                StateOfOrigin = staff.StateOfOrigin,
                Street = staff.Address.Street
            };
            return OperationResult.Successful(staffDto);
        }
    }
}
