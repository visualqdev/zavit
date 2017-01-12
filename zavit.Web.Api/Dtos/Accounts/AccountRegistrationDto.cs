using zavit.Domain.Accounts;

namespace zavit.Web.Api.Dtos.Accounts
{
    public class AccountRegistrationDto
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}