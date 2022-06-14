using SchoolManagementApp.Application.Queries.ResponseDto;
using SchoolManagementApp.Domain.Students;
using Shared.Application.ArchitectureBuilder.Queries;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Queries.Students.FetchStudentsPerClass
{
    public class FetchStudentsPerClassQueryHandler : QueryHandler<Student, Guid, List<StudentResponseDto>, FetchStudentsPerClassQuery>
    {
        public override async Task<ActionResult<List<StudentResponseDto>>> HandleAsync(FetchStudentsPerClassQuery query, CancellationToken cancellationToken = default)
        {
            var students = await QueryContext.FindAsync(x => x.SchoolClass.Id == query.SchoolClassId);
            var response = new List<StudentResponseDto>();
            students.ForEach(x => response.Add(new StudentResponseDto
            {
                SchoolId = x.School.Id,
                Age = x.GetAge(),
                House_Number = x.Address.House_Number,
                Street = x.Address.Street,
                City = x.Address.City,
                FullName = $"{x.FirstName} {x.LastName}",
                Gender = x.Gender,
                LG_Of_Origin = x.LG_Of_Origin,
                PhoneNumber = x.PhoneNumber,
                SchoolClassId = x.SchoolClass.Id,
                StateOfOrigin = x.StateOfOrigin
            }));
            return OperationResult.Successful(response);
        }
    }
}
