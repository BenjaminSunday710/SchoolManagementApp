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
    public class CreateNonAcademicStaffCommandHandler : CommandHandler<CreateNonAcademicStaffCommand, CoreDbContext, CommandResponse>
    {
        private IUserIdentity currentUser;

        public CreateNonAcademicStaffCommandHandler(IUserIdentity userIdentity)
        {
            currentUser = userIdentity;
        }
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateNonAcademicStaffCommand command, CancellationToken cancellationToken = default)
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
            //var currentUser = (IUserIdentity)ServiceProvider.GetService(typeof(IUserIdentity));
            nonAcademicStaff.CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}";

            await Context.NonAcademicStaffRepository.AddAsync(nonAcademicStaff);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to create staff");

            return OperationResult.Successful(new CommandResponse(nonAcademicStaff.Id));
        }
    }
}
