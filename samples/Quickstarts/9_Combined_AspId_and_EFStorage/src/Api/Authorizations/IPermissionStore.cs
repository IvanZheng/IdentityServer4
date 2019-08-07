using System.Threading.Tasks;

namespace Api.Authorizations
{
    public interface IPermissionStore
    {
        Task<bool> IsGrantedAsync(string name,
                                  string providerType,
                                  string providerKey);
    }
}