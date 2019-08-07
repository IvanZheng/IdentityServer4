using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public interface IPermissionValueProvider
    {
        string Name { get; }

        Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context);
    }
}
