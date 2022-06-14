using UserManagement.Domain.Users;

namespace UserManagement.Application.Commands.Users.AuthenticateUser
{
    public class AuthenticatedUserResponse
    {
        public User User { get; set; }
        public string Token { get; set; }
    }
}
