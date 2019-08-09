using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

namespace Api.Authorizations
{
    public class PermissionRequirementHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IPermissionChecker _permissionChecker;

        public PermissionRequirementHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }

        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            PermissionRequirement requirement)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, requirement.PermissionName))
            {
                context.Succeed(requirement);
            }
        }
    }


    public class PermissionScopeRequirementHandler : AuthorizationHandler<PermissionScopeRequirement, string>
    {
        private readonly IPermissionChecker _permissionChecker;

        public PermissionScopeRequirementHandler(IPermissionChecker permissionChecker)
        {
            _permissionChecker = permissionChecker;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, 
                                                             PermissionScopeRequirement requirement, 
                                                             string scope)
        {
            if (await _permissionChecker.IsGrantedAsync(context.User, requirement.PermissionName, scope))
            {
                context.Succeed(requirement);
            }
        }
    }
}
