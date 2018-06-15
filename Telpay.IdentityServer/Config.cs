using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Telpay.IdentityServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource> {
                new ApiResource("api", "My API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
            };
        }


        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                // native clients
                new Client
                {
                    ClientId = "native.hybrid",
                    ClientName = "Native Client (Hybrid with PKCE)",

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RequirePkce = true,
                    AllowedScopes = { "openid", "profile", "email", "api" },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
                new Client
                {
                    ClientId = "server.hybrid",
                    ClientName = "Server-based Client (Hybrid)",

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowedScopes = { "openid", "profile", "email", "api" },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
                new Client
                {
                    ClientId = "native.code",
                    ClientName = "Native Client (Code with PKCE)",

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    RequireClientSecret = false,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    AllowedScopes = { "openid", "profile", "email", "api" },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                },
                new Client
                {
                    ClientId = "server.code",
                    ClientName = "Service Client (Code)",

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.Code,
                    AllowedScopes = { "openid", "profile", "email", "api" },

                    AllowOfflineAccess = true,
                    RefreshTokenUsage = TokenUsage.ReUse
                },

                // server to server
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "api" },
                },

                // implicit (e.g. SPA or OIDC authentication)
                new Client
                {
                    ClientId = "implicit",
                    ClientName = "Implicit Client",
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },
                    FrontChannelLogoutUri = "http://localhost:5000/signout-idsrv", // for testing identityserver on localhost

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", "api" },
                },

                // implicit using reference tokens (e.g. SPA or OIDC authentication)
                new Client
                {
                    ClientId = "implicit.reference",
                    ClientName = "Implicit Client using reference tokens",
                    AllowAccessTokensViaBrowser = true,

                    AccessTokenType = AccessTokenType.Reference,

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", "api" },
                },

                // implicit using reference tokens (e.g. SPA or OIDC authentication)
                new Client
                {
                    ClientId = "implicit.shortlived",
                    ClientName = "Implicit Client using short-lived tokens",
                    AllowAccessTokensViaBrowser = true,

                    AccessTokenLifetime = 70,

                    RedirectUris = { "https://notused" },
                    PostLogoutRedirectUris = { "https://notused" },

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "email", "api" },
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                    new TestUser
                    {
                        SubjectId = "1",
                        Username = "alice",
                        Password = "password"
                    },
                    new TestUser
                    {
                        SubjectId = "2",
                        Username = "bob",
                        Password = "password"
                    }
            };
        }
    }
}
