using System;
using System.Threading.Tasks;
using zavit.Web.Api.Dtos.ExternalAccounts;
using zavit.Web.Authorization.ExternalLogins.Clients;

namespace zavit.Web.Authorization.ExternalLogins.ExternalTokenVerifiers
{
    public class GoogleAccessTokenVerifier : IExternalAccessTokenVerifier
    {
        readonly IGoogleLoginClient _googleLoginClient;
        readonly IExternalLoginsSettings _externalLoginsSettings;

        public GoogleAccessTokenVerifier(IGoogleLoginClient googleLoginClient, IExternalLoginsSettings externalLoginsSettings)
        {
            _googleLoginClient = googleLoginClient;
            _externalLoginsSettings = externalLoginsSettings;
        }

        public async Task<ParsedExternalAccessToken> Verify(string accessToken)
        {
            var jsonResult = await _googleLoginClient.GetTokenInfo(accessToken);

            if (jsonResult == null) return null;

            var parsedToken = new ParsedExternalAccessToken
            {
                user_id = jsonResult["user_id"].ToString(),
                app_id = jsonResult["audience"].ToString()
            };

            if (!string.Equals(_externalLoginsSettings.GoogleClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
            {
                return null;
            }

            return parsedToken;
        }

        public bool CanVerify(string provider)
        {
            return provider == ExternalLoginProvider.Google;
        }
    }
}