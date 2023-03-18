using Shared.Application.ArchitectureBuilder.Commands;
using System.Threading;
using System.Threading.Tasks;
using UserManagement.Domain.Roles;
using UserManagement.Domain.Users;
using UserManagement.Infrastructure.Context;
using Utilities.Result.Util;

namespace UserManagement.Application.Commands.Roles.CreateRole
{
    public class CreateRoleCommandHandler : CommandHandler<CreateRoleCommand, UserManagementDbContext, CommandResponse>
    {
        private IUserIdentity currentUser;

        public CreateRoleCommandHandler(IUserIdentity userIdentity)
        {
            currentUser = userIdentity;
        }
        public async override Task<ActionResult<CommandResponse>> HandleAsync(CreateRoleCommand command, CancellationToken cancellationToken = default)
        {
            var exists = await Context.RoleRepository.ExistsAsync(x => x.Title == command.Title);
            if (exists) return OperationResult.Failed($"permission with title-{command.Title} already exist");

            var role = new Role(command.Title);
            //var currentUser = (IUserIdentity)ServiceProvider.GetService(typeof(IUserIdentity));
            role.CreatedBy = $"{currentUser.FirstName} {currentUser.LastName}";
            await Context.RoleRepository.AddAsync(role);

            var commitStatus = await Context.CommitAsync();
            if (commitStatus.NotSuccessful) return OperationResult.Failed("unable to save role");

            return OperationResult.Successful(new CommandResponse(role.Id));
        }
    }
}
