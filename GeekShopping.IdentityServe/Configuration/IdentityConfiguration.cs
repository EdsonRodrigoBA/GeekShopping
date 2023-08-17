using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GeekShopping.IdentityServe.Configuration
{
    public static class IdentityConfiguration
    {
        public const string Admin = "Admin";
        public const string Client = "Client";
        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
              
           };

        public static IEnumerable<ApiScope> ApiScopes => new List<ApiScope> { 
                new ApiScope("geekShopping", "GeekShopping Server"),
                new ApiScope(name:"read", "Read Data"),
                new ApiScope(name:"write", "Write Data"),
                new ApiScope(name:"delete", "Delete Data"),
        };

        public static IEnumerable<Client> Clients => new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = {new Secret("g33ks00p1ng##s3cr3t3_*****".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials ,
                AllowedScopes = {"read", "write","profile"},
            },
            //new Client
            //{
            //    ClientId = "geekShopping",
            //    ClientSecrets = {new Secret("g33ks00p1ng##s3cr3t3_*****".Sha256()) },
            //    AllowedGrantTypes = GrantTypes.Code ,
            //    AllowedScopes = {"read", "write","profile"},
            //},
            new Client
            {
                ClientId = "geekShopping",
                ClientSecrets = {new Secret("g33ks00p1ng##s3cr3t3_*****".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code ,
                RedirectUris= { "https://localhost:4440/signin-oidc" },
                PostLogoutRedirectUris={ "https://localhost:4440/signout-callback-oidc" },
                AllowedScopes = new List<String>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "geekShopping"

                }
            }
        };


    }
}
