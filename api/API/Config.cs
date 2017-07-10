using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;


namespace API
{
    public class Config
    {
        // scopes define the resources in your system
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("daw_api", "DAW API")
            };
        }

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials
            return new List<Client>
            {
                // implicit flow client (Restaurants SPA)
                new Client
                {
                    ClientId = "daw-app",
                    ClientName = "DAW App",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    RedirectUris = GenerateRedirectUris(),
                    PostLogoutRedirectUris = GenerateLogoutUris(),

                    AllowOfflineAccess = true, // needed for silent renew
                    AllowAccessTokensViaBrowser = true, // needed because is a js app

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "daw_api"
                    }
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
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new List<Claim>
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com")
                    }
                }, 
                new TestUser
                {
                    SubjectId = "3",
                    Username = "pfelix@gmail.com",
                    Password = "123456",
                    Claims = new List<Claim>
                        {
                            new Claim(
                                ClaimTypes.Name,
                                "Pedro Felix",
                                ClaimValueTypes.String
                            ),
                            new Claim(
                                ClaimTypes.Email, 
                                "pfelix@gmail.com", 
                                ClaimValueTypes.String
                            ),
                            new Claim(
                                ClaimTypes.Role, 
                                Roles.Admin, 
                                ClaimValueTypes.String
                            )
                        }
                }
            };
        }

        /*
        |-----------------------------------------------------------------------
        | URIS
        |-----------------------------------------------------------------------
        */
        // Allowed uris
        public static ICollection<string> GetAllowedUris()
        {
            return new List<string>
            {
                "http://localhost:3000"
            };
        }

        private static List<string> GenerateLogoutUris()
        {
            return GetAllowedUris()
                .Select(url => url + "/auth/signout-oidc")
                .ToList();
        }

        private static List<string> GenerateRedirectUris()
        {
            var resultList = new List<string>();

            GetAllowedUris().ToList().ForEach(url =>
            {
                resultList.Add(url + "/auth/signin-oidc");
                resultList.Add(url + "/auth/signout-oidc");
                resultList.Add(url + "/auth/silent-renew-oidc");
            });

            return resultList;
        }
    }
}
