﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>
            {
                new ApiScope("doughnutapi", "Doughnut API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>
            {
                new ApiResource("doughnutapi")
                {
                    Scopes = { "doughnutapi" }
                }
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                // React client
                new Client
                {
                    ClientId = "wewantdoughnuts",
                    ClientName = "We Want Doughnuts",
                    ClientUri = "https://localhost:3000",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    
                    RequireClientSecret = false,

                    RedirectUris =
                    {                        
                        "https://localhost:3000/signin-oidc",                        
                    },

                    PostLogoutRedirectUris = { "https://localhost:3000/signout-oidc" },
                    AllowedCorsOrigins = { "https://localhost:3000" },

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "doughnutapi"
                    },

                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}