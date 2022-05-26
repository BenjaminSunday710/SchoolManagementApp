using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.SchoolClasses;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClass
{
    public class FetchSchoolClassQueryHandler : QueryHandler<SchoolClass, Guid, SchoolClassResponseDto, FetchSchoolClassQuery>
    {
        public async override Task<ActionResult<SchoolClassResponseDto>> HandleAsync(FetchSchoolClassQuery query, CancellationToken cancellationToken = default)
        {
            var schoolClass = await QueryContext.GetByIdAsync(query.Id);
            if (schoolClass == null) return OperationResult.Successful(new SchoolClassResponseDto());

            var classDto = new SchoolClassResponseDto()
            {
                Name = schoolClass.Name,
                SchoolId = schoolClass.School.Id,
                ClassTeacherId = schoolClass.ClassTeacher.Id
            };
            return OperationResult.Successful(classDto);
        }
    }
}
