using LIbrary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace LIbrary.Data
{
    public class ApplicationUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<Reader, IdentityRole>
    {
        public ApplicationUserClaimsPrincipalFactory(UserManager<Reader> userManager,
            RoleManager<IdentityRole> roleManager, IOptions<IdentityOptions> options)
            : base(userManager, roleManager, options)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(Reader user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim("UserName", user.UserName ?? ""));
            identity.AddClaim(new Claim("ImageUrl", user.ImageUrl ?? ""));
            identity.AddClaim(new Claim("Id", user.Id ?? ""));
            return identity;
        }
    }
}
