using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer.Models;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace IdentityServer.Managers
{
    public class UserClaimsPrincipalFactory: UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>
    {
        public UserClaimsPrincipalFactory( 
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager, 
            IOptions<IdentityOptions> optionsAccessor) 
            : base(userManager, roleManager, optionsAccessor)
        {
        }
 
        //protected override async Task<ClaimsIdentity> GenerateClaimsAsync(ApplicationUser user)
        //{
        //    string userId = await this.UserManager.GetUserIdAsync(user);
        //    string userNameAsync = await this.UserManager.GetUserNameAsync(user);
        //    ClaimsIdentity id = new ClaimsIdentity("Identity.Application", this.Options.ClaimsIdentity.UserNameClaimType, this.Options.ClaimsIdentity.RoleClaimType);
        //    id.AddClaim(new Claim(this.Options.ClaimsIdentity.UserIdClaimType, userId));
        //    id.AddClaim(new Claim(this.Options.ClaimsIdentity.UserNameClaimType, userNameAsync));
        //    ClaimsIdentity claimsIdentity;
        //    if (this.UserManager.SupportsUserSecurityStamp)
        //    {
        //        claimsIdentity = id;
        //        string type = this.Options.ClaimsIdentity.SecurityStampClaimType;
        //        claimsIdentity.AddClaim(new Claim(type, await this.UserManager.GetSecurityStampAsync(user)));
        //        claimsIdentity = (ClaimsIdentity) null;
        //        type = (string) null;
        //    }
        //    if (this.UserManager.SupportsUserClaim)
        //    {
        //        claimsIdentity = id;
        //        claimsIdentity.AddClaims((IEnumerable<Claim>) await this.UserManager.GetClaimsAsync(user));
        //        claimsIdentity = (ClaimsIdentity) null;
        //    }
        //    return id;
        //}

        public override async Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
        {
            var principal = await base.CreateAsync(user);
            var identity = (ClaimsIdentity)principal.Identity;
            //var claims = new List<Claim>
            //{
            //    new Claim(JwtClaimTypes.Role, "dataEventRecords"),
            //    new Claim(JwtClaimTypes.Role, "dataEventRecords.user")
            //};
 
            //if (user.DataEventRecordsRole == "dataEventRecords.admin")
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "dataEventRecords.admin"));
            //}
 
            //if (user.IsAdmin)
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "admin"));
            //}
            //else
            //{
            //    claims.Add(new Claim(JwtClaimTypes.Role, "user"));
            //}
 
            //identity.AddClaims(claims);
            return principal;
        }
    }
}
