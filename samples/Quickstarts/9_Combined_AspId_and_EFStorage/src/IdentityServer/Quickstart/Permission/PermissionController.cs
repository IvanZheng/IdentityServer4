using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer.Managers;
using IdentityServer4;
using IdentityServer4.Quickstart.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Quickstart.Permission
{
    [ApiController]
    [SecurityHeaders]
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("permissions")]
    public class PermissionController: Controller
    {
        private readonly PermissionManager _permissionManager;

        public PermissionController(PermissionManager permissionManager)
        {
            _permissionManager = permissionManager;
        }

        [HttpGet]
        public Task<bool> IsGrantedAsync(string name,
                                         string providerType,
                                         string providerKey)
        {
            return _permissionManager.IsGrantedAsync(name,
                                                     providerType,
                                                     providerKey);
        }
    }
}
