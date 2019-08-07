using System;
using System.Security.Claims;

namespace Api.Authorizations
{
    public class PermissionValueCheckContext
    {
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