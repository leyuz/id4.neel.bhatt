using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace Client {
    class Program {
        public static void Main (string[] args) => MainAsync ().GetAwaiter ().GetResult ();

        private static async Task MainAsync () {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync ("http://localhost:5000");
            if (disco.IsError) {
                Console.WriteLine (disco.Error);
                return;
            }
            Console.WriteLine ("Scopes supported below");
            foreach (var scope in disco.ScopesSupported)
                Console.WriteLine ("\t" + scope);

            Console.WriteLine ("Claims supported below");
            foreach (var claim in disco.ClaimsSupported) {
                Console.WriteLine ("\t" + claim);
            }
            // request token

            var result = await makeRequestAsync (
                address: disco.TokenEndpoint,
                clientId: "superclient",
                clientSecret: "secret",
                scope: "Bloomberg");
        }
        static async Task<bool> makeRequestAsync (string address, string clientId, string clientSecret, string scope) {
            var tokenClient = new TokenClient (address: address, clientId: clientId, clientSecret: clientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync (scope: scope);
            if (tokenResponse.IsError) {
                Console.WriteLine (tokenResponse.Error);
                return false;
            }
            Console.WriteLine (tokenResponse.Json);
            Console.WriteLine ("\n\n");
            // call api
            var client = new HttpClient ();
            client.SetBearerToken (tokenResponse.AccessToken);

            var response = await client.GetAsync ("http://localhost:5001/BloombergDLWS");
            if (!response.IsSuccessStatusCode) {
                Console.WriteLine ("Failed");
                Console.WriteLine (response.StatusCode);
            } else {
                Console.WriteLine ("Successful!");
                var content = await response.Content.ReadAsStringAsync ();
                Console.WriteLine ((content));
            }
            return true;
        }
    }
}