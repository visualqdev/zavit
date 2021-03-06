﻿using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace zavit.Web.Authorization.ExternalLogins.Clients.Google
{
    public interface IGoogleLoginClient
    {
        Task<JObject> GetTokenInfo(string accessToken);
        Task<GoogleUserInfoDto> GetUserInfo(string accessToken);
        Task<Stream> GetProfileImage(string pictureUrl);
    }
}