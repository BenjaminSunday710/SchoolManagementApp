using Shared.Domain.Entities;
using UserManagement.Domain.Roles;

namespace UserManagement.Domain.Users
{
    public class UserRole:BaseEntity<int>
    {
        protected UserRole() { }
        public UserRole(User user, Role role)
        {
            user.AssignRole(this);
            role.HasUser(this);
        }

        protected internal virtual void DetachUser()
        {
            this.Role.RemoveUser(this);
            this.User = null;
        }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
