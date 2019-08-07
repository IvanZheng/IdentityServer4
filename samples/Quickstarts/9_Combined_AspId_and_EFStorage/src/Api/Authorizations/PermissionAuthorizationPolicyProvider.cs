﻿using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Api.Authorizations
{
    public class PermissionAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider, IPermissionAuthorizationPolicyProvider
    {
        private readonly ConcurrentDictionary<string, AuthorizationPolicy> _authorizationPolicies;
        private readonly AuthorizationOptions _options;
        private readonly IPermissionDefinitionManager _permissionDefinitionManager;

        public PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options,
                                                     IPermissionDefinitionManager permissionDefinitionManager) : base(options)
        {
            _permissionDefinitionManager = permissionDefinitionManager;
            _authorizationPolicies = new ConcurrentDictionary<string, AuthorizationPolicy>();
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);
            if (policy != null)
            {
                return policy;
            }

            return _authorizationPolicies.GetOrAdd(policyName, key =>
            {
                var permission = _permissionDefinitionManager.GetOrNull(policyName);
                if (permission != null)
                {
                    var policyBuilder = new AuthorizationPolicyBuilder(Array.Empty<string>());
                    policyBuilder.Requirements.Add(new PermissionRequirement(policyName));
                    return policyBuilder.Build();
                }

                return default;
            });
        }
    }
}