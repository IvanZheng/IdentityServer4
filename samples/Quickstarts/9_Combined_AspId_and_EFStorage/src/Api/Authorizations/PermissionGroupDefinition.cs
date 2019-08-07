using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public class PermissionGroupDefinition
    {
        public string Name { get; }
        public Dictionary<string, object> Properties { get; }
        public IReadOnlyList<PermissionDefinition> Permissions => _permissions.ToImmutableList();
        private readonly List<PermissionDefinition> _permissions;


        public PermissionGroupDefinition(string name)
        {
            Name = name;
            _permissions = new List<PermissionDefinition>();
            Properties = new Dictionary<string, object>();
        }

        public object this[string name]
        {
            get => Properties.TryGetValue(name, out var value) ? value : default;
            set => Properties[name] = value;
        }


        public virtual PermissionDefinition AddPermission(string appId, string name, Dictionary<string, object> properties = null)
        {
            var permission = new PermissionDefinition(appId, name, properties);

            _permissions.Add(permission);

            return permission;
        }
    }
}
