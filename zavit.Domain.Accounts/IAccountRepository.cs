namespace zavit.Domain.Accounts
{
    public interface IAccountRepository
    {
        Account Get(string userName);
        void Save(Account account);
        bool AccountExists(string username);
    }
}