using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using zavit.Infrastructure.Ioc;
using zavit.Web.Core;
using zavit.Web.Core.Authorization;

namespace zavit.Web.Api
{
    public class OAuthConfig
    {
        public static void Register(IAppBuilder app, Container container)
        {
            var accessAuthorizationServerProvider = container.Resolve<AccessAuthorizationServerProvider>();

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = accessAuthorizationServerProvider
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}