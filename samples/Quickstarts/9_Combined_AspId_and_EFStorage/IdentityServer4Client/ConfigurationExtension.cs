﻿using System;
using System.Net.Http;
using IdentityModel.Client;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4Client.Authorizations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Refit;

namespace IdentityServer4Client
{
    public static class ConfigurationExtension
    {
        public static IServiceCollection AddHttpServiceClient<TClient, TApiHttpHandler>(this IServiceCollection services,
                                                                                        Action<IServiceProvider, HttpClient> configureHttpClient,
                                                                                        RefitSettings refitSettings = null)
            where TClient : class
            where TApiHttpHandler : DelegatingHandler
        {
            return services.AddTransient<TApiHttpHandler>()
                           .AddRefitClient<TClient>(refitSettings)
                           .AddHttpMessageHandler<TApiHttpHandler>()
                           .ConfigureHttpClient(configureHttpClient)
                           .Services;
        }

        public static IServiceCollection AddIdentityServerLocalApiClient(this IServiceCollection services)
        {
            services.AddHttpServiceClient<IPermissionStore, PermissionStoreHttpHandler>((provider, httpClient) =>
            {
                var options = provider.GetService<IOptionsMonitor<IdentityServerAuthenticationOptions>>().Get("Bearer");
                httpClient.BaseAddress = new Uri(options.Authority);
            });
            return services;
        }
    }
}