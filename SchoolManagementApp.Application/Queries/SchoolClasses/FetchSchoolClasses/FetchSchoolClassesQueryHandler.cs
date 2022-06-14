using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.SchoolClasses;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.SchoolClasses.FetchSchoolClasses
{
    public class FetchSchoolClassesQueryHandler : QueryHandler<SchoolClass, Guid, List<SchoolClassResponseDto>, FetchSchoolClassesQuery>
    {
        public override async Task<ActionResult<List<SchoolClassResponseDto>>> HandleAsync(FetchSchoolClassesQuery query, CancellationToken cancellationToken = default)
        {
            var schoolClasses = await QueryContext.FindAsync(x => x.School.Id == query.SchoolId);
            var response = new List<SchoolClassResponseDto>();
            schoolClasses.ForEach(x => response.Add(new SchoolClassResponseDto
            {
                SchoolId = x.School.Id,
                ClassTeacherId = x.ClassTeacher.Id,
                Name = x.Name
            }));
            return OperationResult.Successful(response);
        }
    }
}
