// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvcCore()
                .AddAuthorization(options =>
                {
                    options.AddPolicy("policy1", builder =>
                    {
                        // require scope1
                        builder.RequireScope("api1");
                    });
                    options.AddPolicy("policy2", builder =>
                    {
                        builder.RequireScope("api2");
                    });
                })
                .AddJsonFormatters();

            //services.AddAuthentication("Bearer")
            //        .AddJwtBearer("Bearer", options =>
            //        {
            //            options.Authority = "http://localhost:5000";
            //            options.RequireHttpsMetadata = false;

            //            options.Audience = "api1";
            //        });

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api1";
                    options.ApiSecret = "api1Secret1".ToSha256();
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins("http://localhost:5003")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}