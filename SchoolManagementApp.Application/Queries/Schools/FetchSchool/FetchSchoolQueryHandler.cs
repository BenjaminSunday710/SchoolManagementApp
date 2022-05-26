using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Schools;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Schools.FetchSchool
{
    public class FetchSchoolQueryHandler : QueryHandler<School, Guid, SchoolResponseDto, FetchSchoolQuery>
    {
        public async override Task<ActionResult<SchoolResponseDto>> HandleAsync(FetchSchoolQuery query, CancellationToken cancellationToken = default)
        {
            var school = await QueryContext.GetByIdAsync(query.Id);
            if (school == null) return OperationResult.Successful(new SchoolResponseDto());

            var schoolDto = new SchoolResponseDto()
            {
                Id = school.Id,
                City = school.Location.City,
                Street = school.Location.Street,
                House_Number = school.Location.House_Number,
                Name = school.Name
            };
            return OperationResult.Successful(schoolDto);
        }
    }
}
