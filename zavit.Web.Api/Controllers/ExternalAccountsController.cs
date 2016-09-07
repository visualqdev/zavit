﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using zavit.Domain.Accounts;
using zavit.Domain.Clients;
using zavit.Domain.ExternalAccounts;
using zavit.Web.Api.Dtos.ExternalAccounts;
using zavit.Web.Api.HttpActionResults;
using zavit.Web.Authorization.ExternalLogins;

namespace zavit.Web.Api.Controllers
{
    public class ExternalAccountsController : ApiController
    {
        readonly IClientRepository _clientRepository;
        readonly IExternalAccountsRepository _externalAccountsRepository;
        readonly IExternalLoginsSettings _externalLoginsSettings;
        readonly IAccountRepository _accountRepository;
        readonly IExternalAccountService _externalAccountService;

        public ExternalAccountsController(IClientRepository clientRepository, IExternalAccountsRepository externalAccountsRepository, IExternalLoginsSettings externalLoginsSettings, IAccountRepository accountRepository, IExternalAccountService externalAccountService)
        {
            _clientRepository = clientRepository;
            _externalAccountsRepository = externalAccountsRepository;
            _externalLoginsSettings = externalLoginsSettings;
            _accountRepository = accountRepository;
            _externalAccountService = externalAccountService;
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

            var redirectUriValidationResult = ValidateClientAndRedirectUri(this.Request, ref redirectUri);

            if (!string.IsNullOrWhiteSpace(redirectUriValidationResult))
            {
                return BadRequest(redirectUriValidationResult);
            }

            var externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {
                var authentication = Request.GetOwinContext().Authentication;
                authentication.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                return new AuthenticationChallengeResult(provider, this);
            }

            var hasRegistered = _externalAccountsRepository.CheckIfExists(externalLogin.LoginProvider, externalLogin.ProviderKey);

            redirectUri =
                $"{redirectUri}#/externallogin?externalaccesstoken={externalLogin.ExternalAccessToken}&provider={externalLogin.LoginProvider}&haslocalaccount={hasRegistered}&externalusername={externalLogin.UserName}&externalemail={externalLogin.UserEmail}";

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

            var accessTokenResponse = GenerateLocalAccessTokenResponse(externalAccount.Account);

            return Ok(accessTokenResponse);
        }

        [HttpGet]
        public async Task<IHttpActionResult> ObtainLocalAccessToken(string provider, string externalAccessToken)
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

            //generate access token response
            var accessTokenResponse = GenerateLocalAccessTokenResponse(externalAccount.Account);

            return Ok(accessTokenResponse);

        }

        private JObject GenerateLocalAccessTokenResponse(Account account)
        {

            var tokenExpiration = TimeSpan.FromDays(1);

            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, account.Username));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(tokenExpiration),
            };

            var clientId = 1;

            var ticket = new AuthenticationTicket(identity, props);
            ticket.Properties.Dictionary.Add("as:client_id", clientId.ToString());
            

            var context = new Microsoft.Owin.Security.Infrastructure.AuthenticationTokenCreateContext(Request.GetOwinContext(), OAuthConfig.OAuthAuthorizationServerOptions.AccessTokenFormat, ticket);
            context.Ticket.Properties.Dictionary.Add("client_id", clientId.ToString());
            OAuthConfig.AccessRefreshTokenProvider.Create(context);

            string accessToken;
            try
            {
                accessToken = OAuthConfig.OAuthBearerOptions.AccessTokenFormat.Protect(ticket);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            var tokenResponse = new JObject(
                                        new JProperty("userName", account.Username),
                                        new JProperty("displayName", account.DisplayName),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", tokenExpiration.TotalSeconds.ToString()),
                                        new JProperty(".issued", context.Ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", context.Ticket.Properties.ExpiresUtc.ToString()),
                                        new JProperty("refresh_token", context.Token));

            return tokenResponse;
        }

        private string ValidateClientAndRedirectUri(HttpRequestMessage request, ref string redirectUriOutput)
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

            var match = queryStrings.FirstOrDefault(keyValue => string.Compare(keyValue.Key, key, true) == 0);

            if (string.IsNullOrEmpty(match.Value)) return null;

            return match.Value;
        }

        private async Task<ParsedExternalAccessToken> VerifyExternalAccessToken(string provider, string accessToken)
        {
            ParsedExternalAccessToken parsedToken = null;

            var verifyTokenEndPoint = "";

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

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }
            public string ExternalAccessToken { get; set; }
            public string UserEmail { get; set; }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                var providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (string.IsNullOrEmpty(providerKeyClaim?.Issuer) || string.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name),
                    UserEmail = identity.FindFirstValue(ClaimTypes.Email),
                    ExternalAccessToken = identity.FindFirstValue("ExternalAccessToken"),
                };
            }
        }
    }
}