using SchoolManagementApp.Domain.SharedKernel.Persons;
using SchoolManagementApp.Domain.Students;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.Students.CreateStudent
{
    public class CreateStudentCommandHandler : CommandHandler<CreateStudentCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateStudentCommand command, CancellationToken cancellationToken = default)
        {
            var school = await Context.SchoolRepository.GetByIdAsync(command.SchoolId);

            if (school == null)
                return OperationResult.Failed($"school with Id-{command.SchoolId} not found");

            var schoolClass = await Context.SchoolClassRepository.GetByIdAsync(command.SchoolClassId);

            if (schoolClass == null)
                return OperationResult.Failed($"school class with Id-{command.SchoolClassId} not found");

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

            var student = new Student(person, school, schoolClass);

            await Context.StudentRepository.AddAsync(student);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to create student");

            return OperationResult.Successful(new CommandResponse(student.Id));
        }
    }
}
