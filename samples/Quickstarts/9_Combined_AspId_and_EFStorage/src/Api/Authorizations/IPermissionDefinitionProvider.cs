using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public interface IPermissionDefinitionProvider
    {
        void Define(Dictionary<string, PermissionGroupDefinition> groups);

    }
}
