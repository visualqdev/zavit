using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;

namespace zavit.Web.Api.Dtos.Accounts
{
    public class AccountRegistrationDto : IAccountRegistration
    {
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email => Username;
        public AccountType AccountType => AccountType.Internal;
    }
}