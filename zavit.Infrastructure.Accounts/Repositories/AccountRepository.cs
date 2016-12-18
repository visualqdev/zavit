using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;
using zavit.Infrastructure.Core.ResultCollections;

namespace zavit.Infrastructure.Accounts.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        readonly ISession _session;

        public AccountRepository(ISession session)
        {
            _session = session;
        }

        public Account Get(string userName)
        {
            return _session.QueryOver<Account>().Where(a => a.Username == userName).SingleOrDefault();
        }

        public void Save(Account account)
        {
            _session.Save(account);
            _session.Flush();
        }

        public bool AccountExists(string username)
        {
            return _session.QueryOver<Account>()
                .Where(a => a.Username == username)
                .RowCount() > 0;
        }

        public IList<Account> GetAccounts(IEnumerable<int> accountIds)
        {
            return _session.QueryOver<Account>()
                .WhereRestrictionOn(a => a.Id).IsIn(accountIds.ToArray())
                .List();
        }

        public IResultCollection<Account> Search(string searchTerm, int skip, int take, int requestedByAccountId)
        {
            var results = _session.QueryOver<Account>()
                .Where(a => a.Id != requestedByAccountId)
                .WhereRestrictionOn(a => a.DisplayName).IsLike(searchTerm, MatchMode.Anywhere)
                .OrderBy(a => a.DisplayName).Asc
                .Skip(skip)
                .Take(take + 1)
                .List();

            return new ResultCollection<Account>(results, take);
        }
    }
}