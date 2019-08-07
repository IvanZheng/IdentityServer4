using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public class PermissionStore: IPermissionStore
    {
        public Task<bool> IsGrantedAsync(string name, string providerType, string providerKey)
        {
            return Task.FromResult(true);
        }
    }
}
