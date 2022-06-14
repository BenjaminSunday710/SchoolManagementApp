using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Students;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudents
{
    public class FetchStudentsQueryHandler : QueryHandler<Student, Guid, List<StudentResponseDto>, FetchStudentsQuery>
    {
        public override async Task<ActionResult<List<StudentResponseDto>>> HandleAsync(FetchStudentsQuery query, CancellationToken cancellationToken = default)
        {
            var students = await QueryContext.FindAsync(x => x.School.Id == query.SchoolId);
            var response = new List<StudentResponseDto>();
            students.ForEach(x => response.Add(new StudentResponseDto
            {
                Age = x.GetAge(),
                FullName = $"{x.FirstName} {x.LastName}",
                Gender = x.Gender,
                House_Number = x.Address.House_Number,
                Street = x.Address.Street,
                City = x.Address.City,
                LG_Of_Origin = x.LG_Of_Origin,
                StateOfOrigin = x.StateOfOrigin,
                PhoneNumber = x.PhoneNumber,
                SchoolClassId = x.SchoolClass.Id,
                SchoolId = x.School.Id
            }));

            return OperationResult.Successful(response);
        }
    }
}
