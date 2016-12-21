using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.ExternalAccounts.Registrations
{
    public class ExternalAccountRegistration : IAccountRegistration
    {
        public string Provider { get; set; }
        public string DisplayName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password => null;
        public AccountType AccountType => AccountType.External;
    }
}