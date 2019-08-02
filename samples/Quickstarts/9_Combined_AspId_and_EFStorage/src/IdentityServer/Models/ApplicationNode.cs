using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Models
{
    public class ApplicationNode
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }

        public string TenantId { get; set; }

        public ApplicationNode(){}

        public ApplicationNode(string name, string tenantId)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            TenantId = tenantId;
        }
    }
}
