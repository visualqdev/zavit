using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.ExternalAccounts.Registrations
{
    public class ExternalAccountRegistrationFactory : IExternalAccountRegistrationFactory
    {
        public IAccountRegistration CreateItem(string username, string displayName, string email)
        {
            return new ExternalAccountRegistration
            {
                Username = username,
                DisplayName = displayName,
                Email = email
            };
        }
    }
}