using NHibernate;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using UserManagement.Domain.Permissions;
using UserManagement.Domain.Roles;
using UserManagement.Infrastructure.Repositories;
using Utilities.Result.Util;

namespace UserManagement.Infrastructure.Context
{
    public class UserManagementDbContext : DbContext
    {
        private ISession _session;

        public async override Task<ActionResult> CommitAsync()
        {
            try
            {
                using (var transaction = _session.BeginTransaction())
                {
                    await transaction.CommitAsync();
                    return ActionResult.Success();
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Failed();
            }
        }

        public override void Setup(ISession session)
        {
            _session = session;
            UserRepository = new UserRepository(session);
            RoleRepository = new Repository<Role, Guid>(session);
            PermissionRepository = new Repository<Permission, Guid>(session);
        }

        public UserRepository UserRepository { get; private set; }
        public IRepository<Role, Guid> RoleRepository { get; private set; }
        public IRepository<Permission, Guid> PermissionRepository { get; private set; }
    }
}
