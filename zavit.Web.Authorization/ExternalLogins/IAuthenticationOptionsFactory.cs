using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;

namespace zavit.Web.Authorization.ExternalLogins
{
    public interface IAuthenticationOptionsFactory
    {
        GoogleOAuth2AuthenticationOptions CreateGoogleOAuth2AuthenticationOptions();
        FacebookAuthenticationOptions CreateFacebookAuthenticationOptions();
    }
}