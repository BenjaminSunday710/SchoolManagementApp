using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.AcademicStaffs;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.AcademicStaffs.FetchAcademicStaffSubjects
{
    public class FetchAcademicStaffSubjectsQueryHandler : QueryHandler<AcademicStaff, Guid, List<SubjectResponseDto>, FetchAcademicStaffSubjectsQuery>
    {
        public override async Task<ActionResult<List<SubjectResponseDto>>> HandleAsync(FetchAcademicStaffSubjectsQuery query, CancellationToken cancellationToken = default)
        {
            var response = new List<SubjectResponseDto>();
            var staff = await QueryContext.GetByIdAsync(query.StaffId);
            foreach (var subject in staff.Subjects)
            {
                response.Add(new SubjectResponseDto()
                {
                    ClassName = subject.SchoolClass.Name,
                    Name = subject.Name,
                    Id=subject.Id
                });
            }
            return OperationResult.Successful(response);
        }
    }
}
