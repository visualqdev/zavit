using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using zavit.Web.Authorization.ExternalLogins.AuthenticationProviders;

namespace zavit.Web.Authorization.ExternalLogins
{
    public class AuthenticationOptionsFactory : IAuthenticationOptionsFactory
    {
        readonly IExternalLoginsSettings _externalLoginsSettings;

        public AuthenticationOptionsFactory(IExternalLoginsSettings externalLoginsSettings)
        {
            _externalLoginsSettings = externalLoginsSettings;
        }

        public GoogleOAuth2AuthenticationOptions CreateGoogleOAuth2AuthenticationOptions()
        {
            return new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = _externalLoginsSettings.GoogleClientId,
                ClientSecret = _externalLoginsSettings.GoogleClientSecret,
                Provider = new GoogleAuthProvider()
            };
        }

        public FacebookAuthenticationOptions CreateFacebookAuthenticationOptions()
        {
            var facebookOptions = new FacebookAuthenticationOptions()
            {
                AppId = _externalLoginsSettings.FacebookAppId,
                AppSecret = _externalLoginsSettings.FacebookAppSecret,
                Provider = new FacebookAuthProvider()
            };

            facebookOptions.Scope.Add("email");

            return facebookOptions;
        }
    }
}