using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace zavit.Web.Authorization.ExternalLogins.Clients.Google
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
            var uri = new Uri($"{_externalLoginsSettings.GoogleOauth2ApiUrl}/tokeninfo?access_token={accessToken}");
            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return null;

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return JObject.Load(jsonReader);
            }
        }

        public async Task<GoogleUserInfoDto> GetUserInfo(string accessToken)
        {
            var message = new HttpRequestMessage();
            message.Headers.Add("Authorization", $"Bearer {accessToken}");
            message.RequestUri = new Uri($"{_externalLoginsSettings.GoogleOauth2ApiUrl}/userinfo");
            message.Method = HttpMethod.Get;

            var response = await _client.SendAsync(message);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GoogleUserInfoDto>(json);
        }

        public async Task<Stream> GetProfileImage(string pictureUrl)
        {
            var message = new HttpRequestMessage
            {
                RequestUri = new Uri(pictureUrl),
                Method = HttpMethod.Get
            };

            var response = await _client.SendAsync(message);
            var imageStream = await response.Content.ReadAsStreamAsync();
            return imageStream;
        }
    }
}