using SchoolManagementApp.Domain.Subjects;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.Subjects.CreateSubject
{
    public class CreateSubjectCommandHandler : CommandHandler<CreateSubjectCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateSubjectCommand command, CancellationToken cancellationToken = default)
        {
            var schoolClass = await Context.SchoolClassRepository.GetByIdAsync(command.SchoolClassId);


            if (schoolClass == null)
                return OperationResult.Failed($"class with Id-{command.SchoolClassId} not found");

            var subject = new Subject(command.Name, schoolClass);

            await Context.SubjectRepository.AddAsync(subject);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to create subject");

            return OperationResult.Successful(new CommandResponse(subject.Id));
        }
    }
}
