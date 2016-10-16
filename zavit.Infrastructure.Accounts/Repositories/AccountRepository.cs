using System.Collections.Generic;
using System.Linq;
using NHibernate;
using zavit.Domain.Accounts;

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

        public IList<Account> GetAccounts(IEnumerable<int> participantIds)
        {
            return _session.QueryOver<Account>()
                .WhereRestrictionOn(a => a.Id).IsIn(participantIds.ToArray())
                .List();
        }
    }
}