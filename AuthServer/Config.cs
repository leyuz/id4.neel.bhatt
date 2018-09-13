using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;

namespace AuthServer {
    public class Config {
        // clients that are allowed to access resources from the Auth server 
        public static IEnumerable<Client> GetClients () {
            var clientList = new List<Client> ();
            // client credentials, list of clients
            var superclient = new Client {
                ClientId = "superclient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {
                new Secret (value: "secret".Sha256 (), expiration : null)
                },
                AllowedScopes = { "Bloomberg", "BloombergWebServiceScheduled" },
            };

            var normalclient = new Client {
                ClientId = "normalclient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {
                new Secret (value: "secret".Sha256 (), expiration : null)
                },

                AllowedScopes = { "BloombergWebServiceScheduled" },

            };
            clientList.Add (superclient);
            clientList.Add (normalclient);

            return clientList;
        }
        // API that are allowed to access the Auth server
        public static IEnumerable<ApiResource> GetApiResources () {
            return new List<ApiResource> {
                new ApiResource ("BloombergSFTP", "My API"),
                new ApiResource ("BloombergWebServiceScheduled", "My API"),
                new ApiResource ("Bloomberg", "My API"),
                new ApiResource ("Admin", "My API"),
                new ApiResource ("test", "My API"),
            };

        }
    }
}