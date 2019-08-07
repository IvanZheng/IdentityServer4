using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class CurrentPrincipalAccessor: ICurrentPrincipalAccessor
    {
        private readonly IServiceProvider _serviceProvider;

        public CurrentPrincipalAccessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ClaimsPrincipal GetCurrentPrincipal()
        {
            var httpContextAccessor = _serviceProvider.GetService<IHttpContextAccessor>();

            return httpContextAccessor?.HttpContext?.User ?? ClaimsPrincipal.Current;
        }

        public ClaimsPrincipal Principal => GetCurrentPrincipal();
    }
}
