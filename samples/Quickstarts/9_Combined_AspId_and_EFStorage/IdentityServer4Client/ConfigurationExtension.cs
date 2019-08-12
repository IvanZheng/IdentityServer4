using System;
using System.Collections.Generic;
using System.Text;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4Client.Authorizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace IdentityServer4Client
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddIdentityServerClient(this IServiceCollection services)
        {
            services.AddRefitClient<IPermissionStore>()
                    .ConfigureHttpClient((provider, httpClient) =>
                    {
                        var options = provider.GetService<IOptions<IdentityServerAuthenticationOptions>>();
                        httpClient.BaseAddress = new Uri(options.Value.Authority);
                    });

            return services;
        }
    }
}
