using System;
using System.Threading.Tasks;
using zavit.Web.Api.Dtos.ExternalAccounts;
using zavit.Web.Authorization.ExternalLogins.Clients;

namespace zavit.Web.Authorization.ExternalLogins.ExternalTokenVerifiers
{
    public class FacebookAccessTokenVerifier : IExternalAccessTokenVerifier
    {
        readonly IFacebookLoginClient _facebookLoginClient;
        readonly IExternalLoginsSettings _externalLoginsSettings;

        public FacebookAccessTokenVerifier(IFacebookLoginClient facebookLoginClient, IExternalLoginsSettings externalLoginsSettings)
        {
            _facebookLoginClient = facebookLoginClient;
            _externalLoginsSettings = externalLoginsSettings;
        }

        public async Task<ParsedExternalAccessToken> Verify(string accessToken)
        {
            var jsonResult = await _facebookLoginClient.GetTokenInfo(accessToken);

            if (jsonResult == null) return null;

            var parsedToken = new ParsedExternalAccessToken
            {
                user_id = jsonResult["data"]["user_id"].ToString(),
                app_id = jsonResult["data"]["app_id"].ToString()
            };

            if (!string.Equals(_externalLoginsSettings.FacebookAppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return parsedToken;
        }

        public bool CanVerify(string provider)
        {
            return provider == ExternalLoginProvider.Facebook;
        }
    }
}