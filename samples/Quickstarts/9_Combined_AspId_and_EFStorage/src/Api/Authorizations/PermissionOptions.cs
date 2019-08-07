using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Authorizations
{
    public class PermissionOptions
    {
        public IList<Type> DefinitionProviders { get; }

        public IList<Type> ValueProviders { get; }

        public PermissionOptions()
        {
            ValueProviders = new List<Type>();

            DefinitionProviders = new List<Type>();
        }
    }
}
