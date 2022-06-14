using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Subjects;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Subjects.FetchSubjects
{
    public class FetchSubjectsQueryHandler : QueryHandler<Subject, Guid, List<SubjectResponseDto>>
    {
        public override async Task<ActionResult<List<SubjectResponseDto>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var subjects = await QueryContext.GetAllAsync();
            var response = new List<SubjectResponseDto>();
            subjects.ForEach(x => response.Add(new SubjectResponseDto
            {
                ClassName = x.SchoolClass.Name,
                Name = x.Name,
                Id = x.Id
            }));

            return OperationResult.Successful(response);
        }
    }
}
