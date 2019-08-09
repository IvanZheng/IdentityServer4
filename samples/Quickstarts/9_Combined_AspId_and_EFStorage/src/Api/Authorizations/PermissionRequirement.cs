﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Api.Authorizations
{
    public interface IPermissionRequirement:IAuthorizationRequirement
    {
        string PermissionName { get; }
    }

    public class PermissionRequirement : IPermissionRequirement
    {
        public string PermissionName { get; }

        public PermissionRequirement(string permissionName)
        {
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(permissionName));
            }
            PermissionName = permissionName;
        }
    }

  
    public class PermissionScopeRequirement : IPermissionRequirement
    {
        public string PermissionName { get; }

        public PermissionScopeRequirement(string permissionName)
        {
            if (string.IsNullOrWhiteSpace(permissionName))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(permissionName));
            }
            PermissionName = permissionName;
        }
    }

}
