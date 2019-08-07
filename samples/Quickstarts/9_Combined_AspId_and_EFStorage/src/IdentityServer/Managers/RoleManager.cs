using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using IdentityServer.Models;
using IdentityServerAspNetIdentity.Data;
using IdentityServerAspNetIdentity.Models;
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

        public virtual async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, ApplicationRole role,  CancellationToken cancellationToken = default)
        {
            var context = (Store as RoleStore<ApplicationRole, ApplicationDbContext, string, IdentityUserRole<string>, IdentityRoleClaim<string>>)?.Context ??
                          throw new ArgumentNullException($"store");

            cancellationToken.ThrowIfCancellationRequested();
            if ( user == null)
                throw new ArgumentNullException(nameof (user));
            if (role == null)
            {
                throw new ArgumentNullException(nameof(role));
            }

            context.Set<IdentityUserRole<string>>().Add(new IdentityUserRole<string> {RoleId = role.Id, UserId = user.Id});
            await context.SaveChangesAsync(cancellationToken)
                         .ConfigureAwait(false);
            return IdentityResult.Success;
        }
    }
}