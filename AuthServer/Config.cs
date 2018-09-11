using System.Collections.Generic;
using System.Security.Claims;
using IdentityServer4.Models;

namespace AuthServer {
    public class Config {
        // clients that are allowed to access resources from the Auth server 
        public static IEnumerable<Client> GetClients () {
            // client credentials, list of clients
            var webclient = new Client {
                ClientId = "webclient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {
                new Secret ("secret".Sha256 ())
                },
                AllowedScopes = { "BloombergWebService", "BloombergSFTP" },

            };
            webclient.Claims = new Claim { };
            var clientList = new List<Client> ();
            clientList.Add (webclient);
            return clientList;
        }
        // API that are allowed to access the Auth server
        public static IEnumerable<ApiResource> GetApiResources () {
            return new List<ApiResource> {
                new ApiResource ("BloombergSFTP", "My API"),
                new ApiResource ("BloombergWebService", "My API")
            };
        }
    }
}