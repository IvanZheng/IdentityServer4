using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Authorizations
{
    public static class AuthorizationServiceExtensions
    {
        public static Task<AuthorizationResult> AuthorizePermissionAsync(this IAuthorizationService service,
                                                               ClaimsPrincipal user,
                                                               object resource,
                                                               string permission)
        {
            return service.AuthorizeAsync(user,
                                          resource,
                                          new PermissionScopeRequirement(permission));
        }
    }
}