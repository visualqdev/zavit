using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Domain.ExternalAccounts
{
    public interface IExternalAccountService
    {
        ExternalAccount CreateExternalAccount(ExternalAccountRegistration externalAccountRegistration);
    }
}