using System.Threading.Tasks;

namespace IdentityServer4Client.Authorizations
{
    public interface IPermissionStore
    {
        Task<bool> IsGrantedAsync(string name,
                                  string providerType,
                                  string providerKey,
                                  string tenantId);
    }
}