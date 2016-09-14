using zavit.Domain.Accounts;

namespace zavit.Domain.ExternalAccounts.Registrations
{
    public class NewExternalAccountProvider : INewExternalAccountProvider
    {
        public ExternalAccount Provide(Account account, string loginProvider, string providerKey)
        {
            return new ExternalAccount
            {
                Account = account,
                LoginProvider = loginProvider,
                ProviderKey = providerKey
            };
        }
    }
}