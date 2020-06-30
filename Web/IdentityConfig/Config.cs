using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityConfig
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetSoluction()
        {
            return new[]
            {
               new ApiResource("api1", "MY API")
            };
        }
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = {
                        new Secret("secret".Sha256()),
                    },
                    AllowedScopes = {"api1"}
                }
            };
        }
    }
}
