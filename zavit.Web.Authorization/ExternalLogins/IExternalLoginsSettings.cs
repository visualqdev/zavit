namespace zavit.Web.Authorization.ExternalLogins
{
    public interface IExternalLoginsSettings
    {
        string GoogleClientId { get; }
        string GoogleClientSecret { get; }
        string FacebookAppId { get; }
        string FacebookAppSecret { get; }
        string FacebookAppToken { get; }
        string FacebookGraphApiUrl { get; }
        string GoogleOauth2ApiUrl { get; }
    }
}