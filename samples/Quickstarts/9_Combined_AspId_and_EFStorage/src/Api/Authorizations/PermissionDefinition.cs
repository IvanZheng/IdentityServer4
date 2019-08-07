using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public class PermissionDefinition
    {
        public string Name { get; }

        public string AppId { get; }
        public Dictionary<string, object> Properties { get; }

        public object this[string name]
        {
            get => Properties.TryGetValue(name, out var value) ? value : default;
            set => Properties[name] = value;
        }
        public PermissionDefinition(string appId, string name, Dictionary<string, object> properties)
        {
            Name = name;
            AppId = appId;
            Properties = properties;
        }
    }
}
