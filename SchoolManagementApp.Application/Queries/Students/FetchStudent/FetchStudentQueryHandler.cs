using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Students;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudent
{
    public class FetchStudentQueryHandler : QueryHandler<Student, Guid, StudentResponseDto, FetchStudentQuery>
    {
        public async override Task<ActionResult<StudentResponseDto>> HandleAsync(FetchStudentQuery query, CancellationToken cancellationToken = default)
        {
            var student = await QueryContext.GetByIdAsync(query.Id);
            if (student == null) return OperationResult.Successful(new StudentResponseDto());

            var studentDto = new StudentResponseDto()
            {
                FullName = $"{student.FirstName} {student.LastName}",
                Gender = student.Gender,
                Age = student.GetAge(),
                House_Number = student.Address.House_Number,
                Street = student.Address.Street,
                City = student.Address.City,
                LG_Of_Origin = student.LG_Of_Origin,
                StateOfOrigin = student.StateOfOrigin,
                PhoneNumber = student.PhoneNumber,
                SchoolClassId=student.SchoolClass.Id,
                SchoolId=student.School.Id
            };

            return OperationResult.Successful(studentDto);
        }
    }
}
