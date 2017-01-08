using System.Collections.Generic;
using zavit.Domain.Profiles;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.Accounts
{
    public interface IAccountRepository
    {
        Account Get(string userName);
        void Save(Account account);
        bool AccountExists(string username);
        IList<Account> GetAccounts(IEnumerable<int> accountIds);
        IResultCollection<Account> Search(string searchTerm, int skip, int take, int requestedByAccountId);
        byte[] GetProfileImage(int accountId);
        Profile GetProfile(int accountId);
    }
}