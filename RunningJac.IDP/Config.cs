// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace RunningJac.IDP
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            { 
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource("country", "The country you live in", new List<string>() { "country" }),
                new IdentityResource("roles", "Your role(s)", new [] { "role" })
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new ApiResource[]
            { 
                new ApiResource() 
                {
                    DisplayName = "Cart Craft API",
                    Name = "cartcraftapi",
                    UserClaims = new List<string>() { "role", "country" },
                    Scopes = new List<string>()
                    {
                        "cartcraftapi.read",
                        "cartcraftapi.write"
                    }
                }
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("cartcraftapi.read"),
                new ApiScope("cartcraftapi.write")
            };

        public static IEnumerable<Client> Clients =>
            new Client[] 
            { 
                new Client()
                {
                    ClientName = "Cart Craft",
                    ClientId = "cartcraftclient",
                    AllowedScopes = 
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "country",
                        "cartcraftapi.read",
                        "cartcraftapi.write"
                    },
                    RedirectUris = { "https://localhost:7184/signin-oidc" },
                    AllowedGrantTypes = GrantTypes.Code,
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    PostLogoutRedirectUris = { "https://localhost:7184/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RequireConsent = true
                }
            };
    }
}