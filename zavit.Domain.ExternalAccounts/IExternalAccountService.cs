namespace zavit.Domain.ExternalAccounts
{
    public interface IExternalAccountService
    {
        ExternalAccount CreateExternalAccount(string loginProvider, string externalUserId, string displayName, string email);
    }
}