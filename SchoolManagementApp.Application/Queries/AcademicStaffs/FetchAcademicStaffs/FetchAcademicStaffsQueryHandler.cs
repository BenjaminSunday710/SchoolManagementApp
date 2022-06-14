using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.AcademicStaffs;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaffs
{
    public class FetchAcademicStaffsQueryHandler : QueryHandler<AcademicStaff, Guid, List<AcademicStaffResponseDto>, FetchAcademicStaffsQuery>
    {
        public override async Task<ActionResult<List<AcademicStaffResponseDto>>> HandleAsync(FetchAcademicStaffsQuery query, CancellationToken cancellationToken = default)
        {
            var staffs = await QueryContext.FindAsync(x => x.School.Id == query.SchoolId);
            var response = new List<AcademicStaffResponseDto>();
            staffs.ForEach(staff => response.Add(new AcademicStaffResponseDto
            {
                SchoolId = staff.School.Id,
                Age = staff.GetAge(),
                House_Number = staff.Address.House_Number,
                Street = staff.Address.Street,
                City = staff.Address.City,
                Designation = staff.Designation,
                FullName = $"{staff.FirstName} {staff.LastName}",
                Gender = staff.Gender,
                LG_Of_Origin = staff.LG_Of_Origin,
                PhoneNumber = staff.PhoneNumber,
                StateOfOrigin = staff.StateOfOrigin
            }));
            return OperationResult.Successful(response);
        }
    }
}
