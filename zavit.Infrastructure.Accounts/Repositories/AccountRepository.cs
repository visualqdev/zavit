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
    }
}