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

            // request token
            var tokenClient = new TokenClient (address: disco.TokenEndpoint, clientId: "webclient", clientSecret: "secret");
            foreach (var scope in disco.ScopesSupported)
                Console.WriteLine ("Scope supported:" + scope);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync (scope: "BloombergWebService");

            if (tokenResponse.IsError) {
                Console.WriteLine (tokenResponse.Error);
                return;
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
        }
    }
}