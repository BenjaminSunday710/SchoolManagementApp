using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Security
{
    public class UserIdentity:IUserIdentity
    {
        public UserIdentity(IHttpContextAccessor httpContext)
        {
            Email = httpContext?.HttpContext?.User?.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            int.TryParse(httpContext?.HttpContext?.User?.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value, out var id);

            Roles = httpContext?.HttpContext?.User?.Claims.Where(claim => claim.Type == ClaimTypes.Role)?.Select(x => x.Value).ToList();

            UserId = id;
        }

        public string Email { get; set; }
        public int UserId { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
