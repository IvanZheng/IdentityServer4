using System.Threading.Tasks;
using IdentityModel;

namespace IdentityServer4Client.Authorizations
{
    public class UserPermissionValueProvider : PermissionValueProvider
    {
        public const string ProviderName = "User";

        public override string Name => ProviderName;

        public UserPermissionValueProvider(IPermissionStore permissionStore)
            : base(permissionStore)
        {

        }

        public override async Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context)
        {
            var userId = context.Principal?.FindFirst(JwtClaimTypes.Subject)?.Value;

            if (userId == null || !string.IsNullOrWhiteSpace(context.ScopeId))
            {
                return PermissionGrantResult.Undefined;
            }

            return await PermissionStore.IsGrantedAsync(context.Permission.Name, Name, userId)
                       ? PermissionGrantResult.Granted
                       : PermissionGrantResult.Undefined;
        }
    }
}
