// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
using System.Threading.Tasks;
using Api.Authorizations;
using Api.Security;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthorizationService _authorizationService;

        public IdentityController(IServiceProvider serviceProvider, IAuthorizationService authorizationService)
        {
            _serviceProvider = serviceProvider;
            _authorizationService = authorizationService;
        }

        [Authorize("policy1")]
        [HttpGet]
        public IActionResult Get(string scopeId)
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [Authorize("policy2")]
        //[Authorize(ApiManagementPermissions.Post)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]dynamic data)
        {
            var result = await _authorizationService.AuthorizeAsync(User, (string)data.ScopeId, new PermissionScopeRequirement(ApiManagementPermissions.Post));
            return new JsonResult(data);
        }
    }
}