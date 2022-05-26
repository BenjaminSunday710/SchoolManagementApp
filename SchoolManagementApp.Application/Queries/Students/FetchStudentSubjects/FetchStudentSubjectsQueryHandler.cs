using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Students;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudentSubjects
{
    public class FetchStudentSubjectsQueryHandler : QueryHandler<Student, Guid, List<SubjectResponseDto>, FetchStudentSubjectsQuery>
    {
        public override async Task<ActionResult<List<SubjectResponseDto>>> HandleAsync(FetchStudentSubjectsQuery query, CancellationToken cancellationToken = default)
        {
            var student = await QueryContext.GetByIdAsync(query.StudentId);
            var response = new List<SubjectResponseDto>();
            foreach (var subject in student.Subjects)
            {
                response.Add(new SubjectResponseDto()
                {
                    ClassName = subject.SchoolClass.Name,
                    Name = subject.Name,
                    Id = subject.Id
                });
            }
            return OperationResult.Successful(response);
        }
    }
}
