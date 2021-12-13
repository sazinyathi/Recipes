using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Recipes.Services;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Recipes.Utils
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        private readonly IConfiguration _configuration;

        public ClaimsTransformer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var loginUser = principal.Identity.Name;
            var user = await UserService.FindByUsernameAsync(loginUser);
            // Switch by Config Logins
            if (user == null)
            {
                var adminLogin = _configuration.GetSection("Logins").GetSection("AdminLogin").Value;
                user = await UserService.FindByUsernameAsync(adminLogin);
            }

            var ci = (ClaimsIdentity)principal.Identity;
            ci.AddClaim(new Claim("UserID", user.UserID.ToString()));
            ci.AddClaim(new Claim("Name", $"{user.FirstName} {user.LastName}"));
            ci.AddClaim(new Claim("FirstName", user.FirstName.ToString()));
            ci.AddClaim(new Claim("CostCentreID", user.UserID.ToString()));
            ci.AddClaim(new Claim("Roles", user.UserID.ToString()));
            ci.AddClaim(new Claim("Username", user.Username));
            ci.AddClaim(new Claim("RoleName", user.RoleName));
            ci.AddClaim(new Claim("Email", user.Email));
            ci.AddClaim(new Claim("JobProfile", "Developer"));

            var roleSplit = user.RoleName.Split(",");

            foreach (var item in roleSplit)
            {
                //var c = new Claim(ClaimTypes.Role, item);
                var c = new Claim(ci.RoleClaimType, item);
                ci.AddClaim(c);
            }

            return principal;
        }
    }
}
