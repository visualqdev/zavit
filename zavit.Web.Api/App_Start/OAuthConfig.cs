using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using zavit.Domain.Accounts.Registrations;
using zavit.Infrastructure.Ioc;
using zavit.Web.Authorization;
using zavit.Web.Authorization.ExternalLogins;

namespace zavit.Web.Api
{
    public class OAuthConfig
    {
        public static OAuthBearerAuthenticationOptions OAuthBearerOptions { get; private set; }
        public static AccessRefreshTokenProvider AccessRefreshTokenProvider { get; private set; }

        public static OAuthAuthorizationServerOptions OAuthAuthorizationServerOptions { get; private set; }

        public static void Register(IAppBuilder app, Container container)
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
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
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