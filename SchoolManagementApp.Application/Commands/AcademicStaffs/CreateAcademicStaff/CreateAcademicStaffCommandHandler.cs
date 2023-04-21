using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.SharedKernel.Persons;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.AcademicStaffs.CreateAcademicStaff
{
    public class CreateAcademicStaffCommandHandler : CommandHandler<CreateAcademicStaffCommand, CoreDbContext, CreateAcademicStaffResponse>
    {
        private IUserIdentity currentUser;

        public CreateAcademicStaffCommandHandler(IUserIdentity userIdentity)
        {
            currentUser = userIdentity;
        }
        public async override Task<ActionResult<CreateAcademicStaffResponse>> HandleAsync(CreateAcademicStaffCommand command, CancellationToken cancellationToken = default)
        {
            var school = await Context.SchoolRepository.GetByIdAsync(command.SchoolId);

            if (school == null)
                return OperationResult.Failed($"school with Id-{command.SchoolId} not found");

            var personBuilder = new PersonBuilder();
            personBuilder.SetAddress(command.City, command.Street, command.House_Number);
            personBuilder.SetDateOfBirth(command.DateOfBirth);
            personBuilder.SetFirstName(command.FirstName);
            personBuilder.SetGender(command.Gender);
            personBuilder.SetLastName(command.LastName);
            personBuilder.SetLG_Of_Origin(command.LG_of_Origin);
            personBuilder.SetPhoneNumber(command.PhoneNumber);
            personBuilder.SetStateOfOrigin(command.State_of_Origin);
            var person = personBuilder.Build();

            var academicStaff = new AcademicStaff(person, school, command.Designation);
            academicStaff.EmploymentId = $@"{school.StaffIdFormat}/{school.LastStaffIdIndex + 1}";
            school.LastStaffIdIndex += 1;
            academicStaff.CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}";

            school.EmployAcademicStaff(academicStaff);

            await Context.SchoolRepository.UpdateAsync(school, school.Id);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to employ academic staff");

            var response = new CreateAcademicStaffResponse
            {
                EmploymentId = academicStaff.EmploymentId,
                Id = academicStaff.Id
            };

            return OperationResult.Successful(response);
        }
    }
}
