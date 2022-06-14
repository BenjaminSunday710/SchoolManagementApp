using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using UserManagement.Domain.Roles;
using UserManagement.Domain.Users;

namespace UserManagement.Infrastructure.Security
{
    public class UserIdentity:IUserIdentity
    {
        public UserIdentity(IHttpContextAccessor httpContext)
        {
            Email = httpContext?.HttpContext?.User?.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Email)?.Value;

            FirstName = httpContext?.HttpContext?.User?.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.GivenName)?.Value;
            
            LastName = httpContext?.HttpContext?.User?.Claims
                .FirstOrDefault(claim => claim.Type == "FamilyName")?.Value;

            Guid.TryParse(httpContext?.HttpContext?.User?.Claims
                .FirstOrDefault(claim => claim.Type == ClaimTypes.Sid)?.Value, out var id);
            UserId = id;

            var roleString = httpContext?.HttpContext?.User.Claims.FirstOrDefault(Claim => Claim.Type == "Roles")?.Value;
            Roles = JsonConvert.DeserializeObject<List<RoleString>>(roleString);
        }

        public string Email { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get ; set; }
        public string LastName { get ; set ; }
        public IEnumerable<RoleString> Roles { get ; set ; }
    }
}
