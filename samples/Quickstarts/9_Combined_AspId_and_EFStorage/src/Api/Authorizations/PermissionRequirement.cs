using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Authorizations
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string PermissionName { get; }
        public string ScopeIdParameter { get; set; }

        public PermissionRequirement(string permissionName, string scopeIdParameter = null)
        {
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(permissionName));
            }
            PermissionName = permissionName;
            ScopeIdParameter = scopeIdParameter;
        }
    }
}
