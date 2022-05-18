using SchoolManagementApp.Domain.SchoolClasses;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.SchoolClasses.CreateSchoolClass
{
    public class CreateSchoolClassCommandHandler : CommandHandler<CreateSchoolClassCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateSchoolClassCommand command, CancellationToken cancellationToken = default)
        {
            var school = await Context.SchoolRepository.GetByIdAsync(command.SchoolId);

            if (school == null)
                return OperationResult.Failed($"School with id-{command.SchoolId} not found");

            var schoolClass = new SchoolClass(command.Name, school);

            await Context.SchoolClassRepository.AddAsync(schoolClass);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to complete the transaction");

            return OperationResult.Successful(new CommandResponse(schoolClass.Id));
        }
    }
}
