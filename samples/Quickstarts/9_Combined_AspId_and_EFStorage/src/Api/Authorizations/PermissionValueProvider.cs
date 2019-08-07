using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public abstract class PermissionValueProvider : IPermissionValueProvider
    {
        public abstract string Name { get; }

        protected IPermissionStore PermissionStore { get; }

        protected PermissionValueProvider(IPermissionStore permissionStore)
        {
            PermissionStore = permissionStore;
        }

        public abstract Task<PermissionGrantResult> CheckAsync(PermissionValueCheckContext context);
    }
}
