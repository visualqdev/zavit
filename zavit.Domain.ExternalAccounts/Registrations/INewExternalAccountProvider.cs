using zavit.Domain.Accounts;

namespace zavit.Domain.ExternalAccounts.Registrations
{
    public interface INewExternalAccountProvider
    {
        ExternalAccount Provide(Account account, string loginProvider, string providerKey);
    }
}