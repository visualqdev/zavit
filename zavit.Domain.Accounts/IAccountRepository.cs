using System.Collections.Generic;

namespace zavit.Domain.Accounts
{
    public interface IAccountRepository
    {
        Account Get(string userName);
        void Save(Account account);
        bool AccountExists(string username);
        IList<Account> GetAccounts(IEnumerable<int> accountIds);
    }
}