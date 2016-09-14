using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json.Linq;
using zavit.Domain.Clients;
using zavit.Domain.ExternalAccounts;
using zavit.Web.Api.Dtos.ExternalAccounts;
using zavit.Web.Authorization.ExternalLogins;
using zavit.Web.Authorization.ExternalLogins.LoginData;
using zavit.Web.Authorization.HttpActionResults;

namespace zavit.Web.Authorization.Controllers
{
    public class ExternalAccountsController : ApiController
    {
        readonly IClientRepository _clientRepository;
        readonly IExternalAccountsRepository _externalAccountsRepository;
        readonly IExternalLoginsSettings _externalLoginsSettings;
        readonly IExternalAccountService _externalAccountService;
        readonly ILocalAccessTokenProvider _localAccessTokenProvider;
        readonly IExternalLoginDataProvider _externalLoginDataProvider;

        public ExternalAccountsController(IClientRepository clientRepository, IExternalAccountsRepository externalAccountsRepository, IExternalLoginsSettings externalLoginsSettings, IExternalAccountService externalAccountService, ILocalAccessTokenProvider localAccessTokenProvider, IExternalLoginDataProvider externalLoginDataProvider)
        {
            _clientRepository = clientRepository;
            _externalAccountsRepository = externalAccountsRepository;
            _externalLoginsSettings = externalLoginsSettings;
            _externalAccountService = externalAccountService;
            _localAccessTokenProvider = localAccessTokenProvider;
            _externalLoginDataProvider = externalLoginDataProvider;
        }

        [OverrideAuthentication]
        [HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult ExternalLogin(string provider, string error = null)
        {
            var redirectUri = string.Empty;

            if (error != null)
            {
                return BadRequest(Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new AuthenticationChallengeResult(provider, this);
            }

            var redirectUriValidationResult = ValidateClientAndRedirectUri(ref redirectUri);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
            {
                return BadRequest(redirectUriValidationResult);
            }

            var externalLoginData = _externalLoginDataProvider.Provide(User.Identity as ClaimsIdentity);

            if (externalLoginData == null)
            {
                return InternalServerError();
            }

            if (externalLoginData.LoginProvider != provider)
            {
                var authentication = Request.GetOwinContext().Authentication;
                authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new AuthenticationChallengeResult(provider, this);
            }

            var hasRegistered = _externalAccountsRepository.CheckIfExists(externalLoginData.LoginProvider, externalLoginData.ProviderKey);

            redirectUri =
                $"{redirectUri}#/externallogin?externalaccesstoken={externalLoginData.ExternalAccessToken}&provider={externalLoginData.LoginProvider}&haslocalaccount={hasRegistered}&externalusername={externalLoginData.UserName}&externalemail={externalLoginData.UserEmail}";

            return Redirect(redirectUri);
        }


        [HttpPost]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {

            var verifiedAccessToken = await VerifyExternalAccessToken(model.Provider, model.ExternalAccessToken);
            if (verifiedAccessToken == null)
            {
                return BadRequest("Invalid Provider or External Access Token");
            }

             var hasRegistered = _externalAccountsRepository.CheckIfExists(model.Provider, verifiedAccessToken.user_id);

            if (hasRegistered)
            {
                return new NegotiatedContentResult<string>(HttpStatusCode.Conflict, "External user is already registered", this);
            }

            var externalAccount = _externalAccountService.CreateExternalAccount(model.Provider, verifiedAccessToken.user_id, model.DisplayName, model.Email);

            var accessTokenResponse = _localAccessTokenProvider.GenerateLocalAccessTokenResponse(
                externalAccount.Account,
                Request.GetOwinContext(),
                OAuthConfig.AccessRefreshTokenProvider,
                OAuthConfig.OAuthBearerOptions,
                OAuthConfig.OAuthAuthorizationServerOptions);

            return Ok(accessTokenResponse);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken, int clientId)
        {

            if (string.IsNullOrWhiteSpace(provider) || string.IsNullOrWhiteSpace(externalAccessToken))
            {
                return BadRequest("Provider or external access token is not sent");
            }

            var verifiedAccessToken = await VerifyExternalAccessToken(provider, externalAccessToken);
            if (verifiedAccessToken == null)
            {
                return BadRequest("Invalid Provider or External Access Token");
            }

            var externalAccount = _externalAccountsRepository.Find(provider, verifiedAccessToken.user_id);

            var hasRegistered = externalAccount != null;

            if (!hasRegistered)
            {
                return BadRequest("External user is not registered");
            }

            var accessTokenResponse = _localAccessTokenProvider.GenerateLocalAccessTokenResponse(
                externalAccount.Account, 
                Request.GetOwinContext(), 
                OAuthConfig.AccessRefreshTokenProvider, 
                OAuthConfig.OAuthBearerOptions,
                OAuthConfig.OAuthAuthorizationServerOptions);

            return Ok(accessTokenResponse);

        }

        string ValidateClientAndRedirectUri(ref string redirectUriOutput)
        {

            Uri redirectUri;

            var redirectUriString = GetQueryString(Request, "redirect_uri");

            if (string.IsNullOrWhiteSpace(redirectUriString))
            {
                return "redirect_uri is required";
            }

            var validUri = Uri.TryCreate(redirectUriString, UriKind.Absolute, out redirectUri);

            if (!validUri)
            {
                return "redirect_uri is invalid";
            }

            var clientIdString = GetQueryString(Request, "client_id");
            int clientId;

            if (string.IsNullOrWhiteSpace(clientIdString) || !int.TryParse(clientIdString, out clientId))
            {
                return "client_id is required";
            }

            var client = _clientRepository.FindClient(clientId);

            if (client == null)
            {
                return $"Client_id '{clientId}' is not registered in the system.";
            }

            if (!client.AllowedOrigin.Equals("*") && !string.Equals(client.AllowedOrigin, redirectUri.GetLeftPart(UriPartial.Authority), StringComparison.OrdinalIgnoreCase))
            {
                return $"The given URL is not allowed by Client_id '{clientId}' configuration.";
            }

            redirectUriOutput = redirectUri.AbsoluteUri;

            return string.Empty;

        }

        string GetQueryString(HttpRequestMessage request, string key)
        {
            var queryStrings = request.GetQueryNameValuePairs();

            if (queryStrings == null) return null;

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, StringComparison.OrdinalIgnoreCase) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }

        async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        {
            ParsedExternalAccessToken parsedToken = null;

            string verifyTokenEndPoint;

            if (provider == "Facebook")
            {
                //You can get it from here: https://developers.facebook.com/tools/accesstoken/
                //More about debug_tokn here: http://stackoverflow.com/questions/16641083/how-does-one-get-the-app-access-token-for-debug-token-inspection-on-facebook

                var appToken = _externalLoginsSettings.FacebookAppToken;
                verifyTokenEndPoint = $"https://graph.facebook.com/debug_token?input_token={accessToken}&access_token={appToken}";
            }
            else if (provider == "Google")
            {
                verifyTokenEndPoint = $"https://www.googleapis.com/oauth2/v1/tokeninfo?access_token={accessToken}";
            }
            else
            {
                return null;
            }

            var client = new HttpClient();
            var uri = new Uri(verifyTokenEndPoint);
            var response = await client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                dynamic jObj = (JObject)Newtonsoft.Json.JsonConvert.DeserializeObject(content);

                parsedToken = new ParsedExternalAccessToken();

                if (provider == "Facebook")
                {
                    parsedToken.user_id = jObj["data"]["user_id"];
                    parsedToken.app_id = jObj["data"]["app_id"];

                    if (!string.Equals(_externalLoginsSettings.FacebookAppId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
                else if (provider == "Google")
                {
                    parsedToken.user_id = jObj["user_id"];
                    parsedToken.app_id = jObj["audience"];

                    if (!string.Equals(_externalLoginsSettings.GoogleClientId, parsedToken.app_id, StringComparison.OrdinalIgnoreCase))
                    {
                        return null;
                    }
                }
            }

            return parsedToken;
        }
    }
}