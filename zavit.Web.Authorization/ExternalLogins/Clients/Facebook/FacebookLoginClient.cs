using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace zavit.Web.Authorization.ExternalLogins.Clients.Facebook
{
    public class FacebookLoginClient : IFacebookLoginClient
    {
        readonly IExternalLoginsSettings _externalLoginsSettings;
        readonly HttpClient _client;

        public FacebookLoginClient(IExternalLoginsSettings externalLoginsSettings)
        {
            _externalLoginsSettings = externalLoginsSettings;
            _client = new HttpClient();
        }

        public async Task<JObject> GetTokenInfo(string accessToken)
        {
            var uri = new Uri($"{_externalLoginsSettings.FacebookGraphApiUrl}/debug_token/?input_token={accessToken}&access_token={_externalLoginsSettings.FacebookAppToken}");
            var response = await _client.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return null;

            using (var stream = await response.Content.ReadAsStreamAsync())
            using (var streamReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(streamReader))
            {
                return JObject.Load(jsonReader);
            }
        }

        public async Task<FacebookUserInfoDto> GetUserInfo(string accessToken)
        {
            var message = new HttpRequestMessage();
            message.Headers.Add("Authorization", $"Bearer {accessToken}");
            message.RequestUri = new Uri($"{_externalLoginsSettings.FacebookGraphApiUrl}/me?fields=email,name,id,gender");
            message.Method = HttpMethod.Get;

            var response = await _client.SendAsync(message);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoDto>(json);
        }

        public async Task<byte[]> GetProfileImage(string userId)
        {
            var message = new HttpRequestMessage
            {
                RequestUri = new Uri($"{_externalLoginsSettings.FacebookGraphApiUrl}/{userId}/picture?type=large"),
                Method = HttpMethod.Get
            };

            var response = await _client.SendAsync(message);
            var imageStream = await response.Content.ReadAsByteArrayAsync();
            return imageStream;
        }
    }
}