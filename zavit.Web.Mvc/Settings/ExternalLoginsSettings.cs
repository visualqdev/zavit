using System.Configuration;
using zavit.Web.Authorization.ExternalLogins;

namespace zavit.Web.Mvc.Settings
{
    public class ExternalLoginsSettings : IExternalLoginsSettings
    {
        string _googleClientId;
        public string GoogleClientId => _googleClientId ?? (_googleClientId = ConfigurationManager.AppSettings["Google.Auth.ClientId"]);

        string _googleClientSecret;
        public string GoogleClientSecret => _googleClientSecret ?? (_googleClientSecret = ConfigurationManager.AppSettings["Google.Auth.ClientSecret"]);

        string _facebookAppId;
        public string FacebookAppId => _facebookAppId ?? (_facebookAppId = ConfigurationManager.AppSettings["Facebook.Auth.AppId"]);

        string _facebookAppSecret;
        public string FacebookAppSecret => _facebookAppSecret ?? (_facebookAppSecret = ConfigurationManager.AppSettings["Facebook.Auth.AppSecret"]);

        string _facebookAppToken;
        public string FacebookAppToken => _facebookAppToken ?? (_facebookAppToken = ConfigurationManager.AppSettings["Facebook.Auth.AppToken"]);

        string _facebookGraphApiUrl;
        public string FacebookGraphApiUrl => _facebookGraphApiUrl ?? (_facebookGraphApiUrl = ConfigurationManager.AppSettings["Facebook.Auth.GraphApiUrl"]);

        string _googleOauth2ApiUrl;
        public string GoogleOauth2ApiUrl => _googleOauth2ApiUrl ?? (_googleOauth2ApiUrl = ConfigurationManager.AppSettings["Google.Auth.OAuth2ApiUrl"]);
    }
}