using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public interface IPermissionChecker
    {
        Task<bool> IsGrantedAsync(string name);

        Task<bool> IsGrantedAsync(ClaimsPrincipal claimsPrincipal, string name, string scope = null);
    }
}
