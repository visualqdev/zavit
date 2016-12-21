namespace zavit.Web.Authorization.ExternalLogins.Clients.Google
{
    public class GoogleUserInfoDto
    {
        public string id { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string picture { get; set; }
        public string gender { get; set; }
    }
}