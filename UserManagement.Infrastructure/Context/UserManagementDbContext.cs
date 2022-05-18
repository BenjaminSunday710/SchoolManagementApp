using NHibernate;
using Shared.Infrastructure.Context;
using Shared.Infrastructure.Repositories;
using System;
using System.Threading.Tasks;
using UserManagement.Domain.Permissions;
using UserManagement.Domain.Roles;
using UserManagement.Domain.Users;
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
            UserRepository = new Repository<User, int>(session);
            UserRoleRepository = new Repository<UserRole, int>(session);   
            RoleRepository = new Repository<Role, int>(session);
            RolePermissionRepository = new Repository<RolePermission, int>(session);
            PermissionRepository = new Repository<Permission, int>(session);   
        }

        public IRepository<User,int> UserRepository { get; private set; }
        public IRepository<UserRole,int> UserRoleRepository { get; private set; }
        public IRepository<Role, int> RoleRepository { get; private set; }
        public IRepository<RolePermission, int> RolePermissionRepository { get; private set; }
        public IRepository<Permission,int> PermissionRepository { get; private set; }
    }
}
