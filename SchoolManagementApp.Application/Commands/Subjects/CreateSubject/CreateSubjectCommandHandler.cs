using SchoolManagementApp.Domain.Subjects;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.Subjects.CreateSubject
{
    public class CreateSubjectCommandHandler : CommandHandler<CreateSubjectCommand, CoreDbContext, CommandResponse>
    {
        private IUserIdentity currentUser;

        public CreateSubjectCommandHandler(IUserIdentity userIdentity)
        {
            currentUser = userIdentity;
        }   
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateSubjectCommand command, CancellationToken cancellationToken = default)
        {
            var schoolClass = await Context.SchoolClassRepository.GetByIdAsync(command.SchoolClassId);


            if (schoolClass == null)
                return OperationResult.Failed($"class with Id-{command.SchoolClassId} not found");

            var subject = new Subject(command.Name, schoolClass);

            //var currentUser = (IUserIdentity)ServiceProvider.GetService(typeof(IUserIdentity));
            subject.CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}";

            await Context.SubjectRepository.AddAsync(subject);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to create subject");

            return OperationResult.Successful(new CommandResponse(subject.Id));
        }
    }
}
