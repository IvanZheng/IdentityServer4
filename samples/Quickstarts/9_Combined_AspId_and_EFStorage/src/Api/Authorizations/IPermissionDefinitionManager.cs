using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public interface IPermissionDefinitionManager
    {
        PermissionDefinition Get(string name);

        PermissionDefinition GetOrNull(string name);

        IReadOnlyList<PermissionDefinition> GetPermissions();

        IReadOnlyList<PermissionGroupDefinition> GetGroups();
    }
}
