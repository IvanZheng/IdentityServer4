using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServerAspNetIdentity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Managers
{
    public class RoleManager : RoleManager<ApplicationRole>
    {
        public RoleManager(IRoleStore<ApplicationRole> store,
                           IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
                           ILookupNormalizer keyNormalizer,
                           IdentityErrorDescriber errors,
                           ILogger<RoleManager<ApplicationRole>> logger)
            : base(store, roleValidators, keyNormalizer, errors, logger)
        {
        }

        public virtual Task<ApplicationRole> FindByNameScopeAsync(string roleName, string scopeId)
        {
            return (Store as RoleStore<ApplicationRole, ApplicationDbContext, string, IdentityUserRole<string>, IdentityRoleClaim<string>>)?.Context.Set<ApplicationRole>().FirstOrDefaultAsync(r => r.Name == roleName && r.ScopeId == scopeId);
        }
    }
}