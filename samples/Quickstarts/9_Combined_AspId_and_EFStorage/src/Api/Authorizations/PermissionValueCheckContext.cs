using System;
using System.Security.Claims;
using IdentityModel;

namespace Api.Authorizations
{
    public class PermissionValueCheckContext
    {
        public PermissionValueCheckContext(PermissionDefinition permission,
                                           string userId,
                                           string scopeId = null)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
            Principal = new ClaimsPrincipal(new ClaimsIdentity(new []{new Claim(JwtClaimTypes.Subject, userId)}));
            ScopeId = scopeId;
        }

        public PermissionValueCheckContext(PermissionDefinition permission,
                                           ClaimsPrincipal principal,
                                           string scopeId = null)
        {
            Permission = permission ?? throw new ArgumentNullException(nameof(permission));
            Principal = principal;
            ScopeId = scopeId;
        }

        public PermissionDefinition Permission { get; }
        public ClaimsPrincipal Principal { get; }
        public string ScopeId { get;  }
    }
}