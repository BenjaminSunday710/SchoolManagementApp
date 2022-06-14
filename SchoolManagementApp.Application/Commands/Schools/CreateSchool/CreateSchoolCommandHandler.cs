using SchoolManagementApp.Domain.Schools;
using SchoolManagementApp.Infrastructure.Context;
using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Users;
using Utilities.Result.Util;

namespace SchoolManagementApp.Application.Commands.Schools.CreateSchool
{
    public class CreateSchoolCommandHandler : CommandHandler<CreateSchoolCommand, CoreDbContext, CommandResponse>
    {
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateSchoolCommand command, CancellationToken cancellationToken = default)
        {
            var school = new School(command.Name);
            school.ProvideLocation(command.City, command.Street, command.House_Number);

            var currentUser = (IUserIdentity)ServiceProvider.GetService(typeof(IUserIdentity));
            school.CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}";

            await Context.SchoolRepository.AddAsync(school);
            var commitStatus = await Context.CommitAsync();

            if (commitStatus.NotSuccessful)
                return OperationResult.Failed("Unable to create school");

            return OperationResult.Successful(new CommandResponse(school.Id));
        }
    }
}
