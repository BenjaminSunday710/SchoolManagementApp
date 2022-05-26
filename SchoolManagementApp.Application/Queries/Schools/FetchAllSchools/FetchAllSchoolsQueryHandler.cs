using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Schools;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Schools.FetchAllSchools
{
    public class FetchAllSchoolsQueryHandler : QueryHandler<School, Guid, List<SchoolResponseDto>>
    {
        public override async Task<ActionResult<List<SchoolResponseDto>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var response = new List<SchoolResponseDto>();

            var schools = await QueryContext.GetAllAsync();
            if (schools == null) return OperationResult.Successful(response);

            schools.ForEach(sch => response.Add(new SchoolResponseDto()
            {
                City = sch.Location.City,
                House_Number = sch.Location.House_Number,
                Id = sch.Id,
                Name = sch.Name,
                Street = sch.Location.Street
            }));
            return OperationResult.Successful(response);
        }
    }
}
