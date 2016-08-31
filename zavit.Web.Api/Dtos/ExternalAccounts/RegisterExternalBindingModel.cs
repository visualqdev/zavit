namespace zavit.Web.Api.Dtos.ExternalAccounts
{
    public class RegisterExternalBindingModel
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Provider { get; set; }
        public string ExternalAccessToken { get; set; }
    }
}