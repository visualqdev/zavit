using zavit.Domain.Accounts.Registrations;

namespace zavit.Web.Api.Dtos.Accounts
{
    public class AccountRegistrationDto : IAccountRegistration
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}