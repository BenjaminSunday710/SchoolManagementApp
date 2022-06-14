using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.NonAcademicStaffs;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.NonAcademicStaffs.FetchNonAcademicStaffs
{
    public class FetchNonAcademicStaffsQueryHandler : QueryHandler<NonAcademicStaff, Guid, List<NonAcademicStaffResponseDto>, FetchNonAcademicStaffsQuery>
    {
        public override async Task<ActionResult<List<NonAcademicStaffResponseDto>>> HandleAsync(FetchNonAcademicStaffsQuery query, CancellationToken cancellationToken = default)
        {
            var staffs = await QueryContext.FindAsync(x => x.School.Id == query.SchoolId);
            var response = new List<NonAcademicStaffResponseDto>();
            staffs.ForEach(staff => response.Add(new NonAcademicStaffResponseDto
            {
                SchoolId = staff.Id,
                Age = staff.GetAge(),
                House_Number = staff.Address.House_Number,
                Street = staff.Address.Street,
                City = staff.Address.City,
                LG_Of_Origin = staff.LG_Of_Origin,
                StateOfOrigin = staff.StateOfOrigin,
                Designation = staff.Designation,
                FullName = $"{staff.FirstName} {staff.LastName}",
                Gender = staff.Gender,
                PhoneNumber = staff.PhoneNumber,
                StaffUnit=staff.Unit
            }));

            return OperationResult.Successful(response);
        }
    }
}
