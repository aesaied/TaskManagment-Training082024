using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using TaskManagment.Entities;

namespace TaskManagment.Services
{
    public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser, AppRole>
    {
        public AdditionalUserClaimsPrincipalFactory(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IOptions<IdentityOptions> options) : base(userManager, roleManager, options)
        {
        }


        public override async Task<ClaimsPrincipal> CreateAsync(AppUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.GivenName, user.FullName));
            claims.Add(new Claim(ClaimTypes.Country, user.CountryId.ToString()));



            identity.AddClaims(claims);
            return principal;
        }
    }
}
