using SchoolManagementApp.Domain.AcademicStaffs;
using SchoolManagementApp.Domain.SharedKernel.Persons;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.AcademicStaffs.CreateAcademicStaff
{
    public class CreateAcademicStaffCommandHandler : CommandHandler<CreateAcademicStaffCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateAcademicStaffCommand command, CancellationToken cancellationToken = default)
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

            await Context.AcademicStaffRepository.AddAsync(academicStaff);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to create staff");

            return OperationResult.Successful(new CommandResponse(academicStaff.Id));
        }
    }
}
