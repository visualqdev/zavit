namespace zavit.Domain.Accounts
{
    public interface IAccountRepository
    {
        Account Get(string userName);
    }
}