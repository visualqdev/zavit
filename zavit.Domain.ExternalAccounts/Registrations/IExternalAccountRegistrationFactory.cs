using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.ExternalAccounts.Registrations
{
    public interface IExternalAccountRegistrationFactory
    {
        IAccountRegistration CreateItem(string username, string displayName, string email);
    }
}