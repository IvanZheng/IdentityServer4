using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.CAS;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class CASExtensions
    {
        public static AuthenticationBuilder AddCAS(this AuthenticationBuilder builder)
            => builder.AddCAS(CASDefaults.AuthenticationScheme, _ => { });

        public static AuthenticationBuilder AddCAS(this AuthenticationBuilder builder, Action<CASOptions> configureOptions)
            => builder.AddCAS(CASDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddCAS(this AuthenticationBuilder builder, string authenticationScheme, Action<CASOptions> configureOptions)
            => builder.AddCAS(authenticationScheme, CASDefaults.DisplayName, configureOptions);

        public static AuthenticationBuilder AddCAS(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<CASOptions> configureOptions)
            => builder.AddOAuth<CASOptions, CASHandler>(authenticationScheme, displayName, configureOptions);
    }
}
