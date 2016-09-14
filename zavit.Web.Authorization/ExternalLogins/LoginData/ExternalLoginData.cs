namespace zavit.Web.Authorization.ExternalLogins.LoginData
{
    public class ExternalLoginData
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public string UserName { get; set; }
        public string ExternalAccessToken { get; set; }
        public string UserEmail { get; set; }
    }
}