namespace zavit.Domain.ExternalAccounts
{
    public interface IExternalAccountsRepository
    {
        bool CheckIfExists(string loginProvider, string providerKey);
        void Save(ExternalAccount externalAccount);
        ExternalAccount Find(string loginProvider, string providerKey);
    }
}