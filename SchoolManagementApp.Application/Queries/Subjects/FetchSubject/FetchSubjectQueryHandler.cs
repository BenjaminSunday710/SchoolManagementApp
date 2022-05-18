using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Subjects;
using Shared.Application.ArchitectureBuilder.Queries;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Subjects.FetchSubject
{
    public class FetchSubjectQueryHandler : QueryHandler<Subject, int, SubjectResponseDto, FetchSubjectQuery>
    {
        public async override Task<ActionResult<SubjectResponseDto>> HandleAsync(FetchSubjectQuery query, CancellationToken cancellationToken = default)
        {
            var subject = await QueryContext.GetByIdAsync(query.Id);
            if (subject == null) return OperationResult.Successful(new SubjectResponseDto());
            var subjectDto = new SubjectResponseDto(subject.Name, subject.SchoolClass.Name);

            return OperationResult.Successful(subjectDto);
        }
    }
}
