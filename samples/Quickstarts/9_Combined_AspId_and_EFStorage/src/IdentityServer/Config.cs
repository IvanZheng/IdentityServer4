using System;
using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace IdentityServerAspNetIdentity
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource> {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource(JwtClaimTypes.Role, JwtClaimTypes.Role, new []{JwtClaimTypes.Role})
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API", new List<string>{JwtClaimTypes.Role})
                {
                    ApiSecrets = new List<Secret> 
                    {
                        new Secret("api1Secret1".Sha256(), "api1Secret1", DateTime.Now.AddMinutes(1)),
                        new Secret("api1Secret2".Sha256(), "api1Secret2")

                    },
                    Scopes = new List<Scope>
                    {
                        new Scope("api1"), new Scope("api2"), new Scope("apiall")
                    }
                }
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256(), "secret1", DateTime.Now.AddMinutes(1)), 
                        new Secret("secret2".Sha256(), "secret2")
                    },

                    // scopes that client has access to
                    AllowedScopes = {
                        "api1", 
                        "api2",
                        "apiall",
                        JwtClaimTypes.Role
                    }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client", 
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    AllowedScopes = {"api1"}
                },
                // OpenID Connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    ClientSecrets = {new Secret("secret".Sha256())},
                    RedirectUris = {"http://localhost:5002/signin-oidc"},
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},
                    AllowedScopes = {
                        IdentityServerConstants.StandardScopes.OpenId, 
                        IdentityServerConstants.StandardScopes.Profile,
                        JwtClaimTypes.Role,
                        "api1",
                        "api2"
                    },
                    AllowOfflineAccess = true
                },
                // JavaScript Client
                new Client
                {
                    ClientId = "js",
                    ClientName = "JavaScript Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    RedirectUris = {"http://localhost:5003/callback.html"},
                    PostLogoutRedirectUris = {"http://localhost:5003/index.html"},
                    AllowedCorsOrigins = {"http://localhost:5003"},
                    AllowedScopes = {IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, "api1"}
                }
            };
        }
    }
}