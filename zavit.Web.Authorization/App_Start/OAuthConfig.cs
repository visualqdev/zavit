using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using zavit.Domain.Accounts.Registrations;
using zavit.Infrastructure.Core;
using zavit.Web.Authorization.ExternalLogins;

namespace zavit.Web.Authorization
{
    public class OAuthConfig
    {
        public static TimeSpan AccessTokenLifeTime = TimeSpan.FromMinutes(2);

        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static AccessRefreshTokenProvider AccessRefreshTokenProvider { get; private set; }

        public static OAuthAuthorizationServerOptions OAuthAuthorizationServerOptions { get; private set; }

        public static void Register(IAppBuilder app, IContainer container)
        {
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
            OAuthBearerOptions = new OAuthBearerAuthenticationOptions();

            var accessAuthorizationServerProvider = 
                new AccessAuthorizationServerProvider(
                    container.Resolve<IAccountRepositoryFactory>(), 
                    container.Resolve<IClientRepositoryFactory>(),
                    container.Resolve<IAccountSecurity>());

            AccessRefreshTokenProvider = 
                new AccessRefreshTokenProvider(
                    container.Resolve<IRefreshTokenRepositoryFactory>(), 
                    container.Resolve<IRefreshTokenProviderFactory>());

            OAuthAuthorizationServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = AccessTokenLifeTime,
                Provider = accessAuthorizationServerProvider,
                RefreshTokenProvider = AccessRefreshTokenProvider
            };

            app.UseOAuthAuthorizationServer(OAuthAuthorizationServerOptions);
            app.UseOAuthBearerAuthentication(OAuthBearerOptions);

            var authenticationOptionsFactory = container.Resolve<IAuthenticationOptionsFactory>();
            app.UseGoogleAuthentication(authenticationOptionsFactory.CreateGoogleOAuth2AuthenticationOptions());
            app.UseFacebookAuthentication(authenticationOptionsFactory.CreateFacebookAuthenticationOptions());
        }
    }
}