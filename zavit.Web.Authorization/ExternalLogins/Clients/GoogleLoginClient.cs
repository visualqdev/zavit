using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace zavit.Web.Authorization.ExternalLogins.Clients
{
    public class GoogleLoginClient : IGoogleLoginClient
    {
        readonly IExternalLoginsSettings _externalLoginsSettings;
        readonly HttpClient _client;

        public GoogleLoginClient(IExternalLoginsSettings externalLoginsSettings)
        {
            _externalLoginsSettings = externalLoginsSettings;
            _client = new HttpClient();
        }

        public async Task<JObject> GetTokenInfo(string accessToken)
        {
            var uri = new Uri($"{_externalLoginsSettings.GoogleOauth2ApiUrl}/tokeninfo?input_token={accessToken}");
            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return null;

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return JObject.Load(jsonReader);
            }
        }
    }
}