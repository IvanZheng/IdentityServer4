// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;
using System.Linq;
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

        public IdentityController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [Authorize("policy1")]
        [HttpGet]
        public IActionResult Get(string scopeId)
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }

        [Authorize("policy2")]
        [Authorize(ApiManagementPermissions.Post + ":ScopeId")]
        [HttpPost]
        public IActionResult Post([FromBody]object data)
        {
            return new JsonResult(data);
        }
    }
}