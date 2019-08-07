using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public interface IPermissionValueProviderManager
    {
        IReadOnlyList<IPermissionValueProvider> ValueProviders { get; }
    }
}
