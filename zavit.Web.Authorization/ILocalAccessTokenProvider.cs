using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using zavit.Domain.Accounts;

namespace zavit.Web.Authorization
{
    public interface ILocalAccessTokenProvider
    {
        JObject GenerateLocalAccessTokenResponse(Account account, IOwinContext owinContext, AccessRefreshTokenProvider accessRefreshTokenProvider, OAuthBearerAuthenticationOptions oauthBearerOptions, OAuthAuthorizationServerOptions oauthAuthorizationServerOptions);
    }
}