using System;
using System.Globalization;
using System.Security.Claims;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using zavit.Domain.Accounts;

namespace zavit.Web.Authorization
{
    public class LocalAccessTokenProvider : ILocalAccessTokenProvider
    {
        const string ExternalClientId = "1";

        public JObject GenerateLocalAccessTokenResponse(Account account, IOwinContext owinContext, AccessRefreshTokenProvider accessRefreshTokenProvider, OAuthBearerAuthenticationOptions oauthBearerOptions, OAuthAuthorizationServerOptions oauthAuthorizationServerOptions, TimeSpan accessTokenLifeTime)
        {
            var identity = new ClaimsIdentity(OAuthDefaults.AuthenticationType);

            identity.AddClaim(new Claim(ClaimTypes.Name, account.Username));
            identity.AddClaim(new Claim("role", "user"));

            var props = new AuthenticationProperties()
            {
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.Add(accessTokenLifeTime),
            };

            var ticket = new AuthenticationTicket(identity, props);
            ticket.Properties.Dictionary.Add("as:client_id", ExternalClientId);


            var context = new Microsoft.Owin.Security.Infrastructure.AuthenticationTokenCreateContext(owinContext, oauthAuthorizationServerOptions.AccessTokenFormat, ticket);
            context.Ticket.Properties.Dictionary.Add("client_id", ExternalClientId);
            accessRefreshTokenProvider.Create(context);

            var accessToken = oauthBearerOptions.AccessTokenFormat.Protect(ticket);

            var tokenResponse = new JObject(
                                        new JProperty("userName", account.Username),
                                        new JProperty("displayName", account.DisplayName),
                                        new JProperty("access_token", accessToken),
                                        new JProperty("accountId", account.Id.ToString()),
                                        new JProperty("token_type", "bearer"),
                                        new JProperty("expires_in", accessTokenLifeTime.TotalSeconds.ToString(CultureInfo.InvariantCulture)),
                                        new JProperty(".issued", context.Ticket.Properties.IssuedUtc.ToString()),
                                        new JProperty(".expires", context.Ticket.Properties.ExpiresUtc.ToString()),
                                        new JProperty("refresh_token", context.Token));

            return tokenResponse;
        }
    }
}