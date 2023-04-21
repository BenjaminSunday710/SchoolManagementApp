using SchoolManagementApp.Application.Commands.AcademicStaffs.CreateAcademicStaff;
using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.NonAcademicStaffs;
using SchoolManagementApp.Domain.SharedKernel.Persons;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.NonAcademicStaffs.CreateNonAcademicStaff
{
    public class CreateNonAcademicStaffCommandHandler : CommandHandler<CreateNonAcademicStaffCommand, CoreDbContext, CreateNonAcademicStaffResponse>
    {
        private IUserIdentity currentUser;

        public CreateNonAcademicStaffCommandHandler(IUserIdentity userIdentity)
        {
            currentUser = userIdentity;
        }
        public async override Task<ActionResult<CreateNonAcademicStaffResponse>> HandleAsync(CreateNonAcademicStaffCommand command, CancellationToken cancellationToken = default)
        {
            var school = await Context.SchoolRepository.GetByIdAsync(command.SchoolId);

            if (school == null)
                return OperationResult.Failed($"school with id-{command.SchoolId} not found");

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

            var nonAcademicStaff = new NonAcademicStaff(person, school, command.Unit, command.Designation);
            nonAcademicStaff.CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}";

            nonAcademicStaff.EmploymentId = $@"{school.StaffIdFormat}/{school.LastStaffIdIndex + 1}";
            school.LastStaffIdIndex += 1;

            school.EmployNonAcademicStaff(nonAcademicStaff);

            await Context.SchoolRepository.UpdateAsync(school, school.Id);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to employ non-academic staff");

            var response = new CreateNonAcademicStaffResponse
            {
                EmploymentId = nonAcademicStaff.EmploymentId,
                Id = nonAcademicStaff.Id
            };

            return OperationResult.Successful(response);
        }
    }
}
