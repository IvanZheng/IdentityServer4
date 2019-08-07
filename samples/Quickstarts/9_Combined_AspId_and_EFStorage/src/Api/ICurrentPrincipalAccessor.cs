﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api
{
    public interface ICurrentPrincipalAccessor
    {
        ClaimsPrincipal Principal { get; }
    }
}