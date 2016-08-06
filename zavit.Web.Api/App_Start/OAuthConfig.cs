using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using zavit.Domain.Accounts.Registrations;
using zavit.Infrastructure.Ioc;
using zavit.Web.Core.Authorization;

namespace zavit.Web.Api
{
    public class OAuthConfig
    {
        public static void Register(IAppBuilder app, Container container)
        {
            var accessAuthorizationServerProvider = 
                new AccessAuthorizationServerProvider(
                    container.Resolve<IAccountRepositoryFactory>(), 
                    container.Resolve<IClientRepositoryFactory>(),
                    container.Resolve<IAccountSecurity>());

            var accessRefreshTokenProvider = 
                new AccessRefreshTokenProvider(
                    container.Resolve<IRefreshTokenRepositoryFactory>(), 
                    container.Resolve<IRefreshTokenProviderFactory>());

            var oAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = accessAuthorizationServerProvider,
                RefreshTokenProvider = accessRefreshTokenProvider
            };

            app.UseOAuthAuthorizationServer(oAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }
}