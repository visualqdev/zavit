using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.SqlCommand;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
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
            return _session.QueryOver<Account>()
                .Fetch(a => a.Profile).Eager
                .Where(a => a.Username == userName).SingleOrDefault();
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
                .Fetch(a => a.Profile).Eager
                .WhereRestrictionOn(a => a.Id).IsIn(accountIds.ToArray())
                .List();
        }

        public IResultCollection<Account> Search(string searchTerm, int skip, int take, int requestedByAccountId)
        {
            Profile profileAlias = null;

            var results = _session.QueryOver<Account>()
                .Fetch(a => a.Profile).Eager
                .JoinAlias(a => a.Profile, () => profileAlias, JoinType.InnerJoin)
                .Where(a => a.Id != requestedByAccountId)
                .WhereRestrictionOn(() => profileAlias.DisplayName).IsLike(searchTerm, MatchMode.Anywhere)
                .OrderBy(() => profileAlias.DisplayName).Asc
                .Skip(skip)
                .Take(take + 1)
                .List();

            return new ResultCollection<Account>(results, take);
        }

        public byte[] GetProfileImage(int accountId)
        {
            Profile profileAlias = null;
            ProfileImage profileImageAlias = null;

            return _session.QueryOver<Account>()
                .JoinAlias(a => a.Profile, () => profileAlias, JoinType.InnerJoin)
                .JoinAlias(() => profileAlias.ProfileImage, () => profileImageAlias, JoinType.InnerJoin)
                .Where(a => a.Id == accountId)
                .SelectList(list => list.Select(() => profileImageAlias.ImageFile))
                .SingleOrDefault<byte[]>();
        }

        public Profile GetProfile(int accountId)
        {
            var profile = _session.QueryOver<Account>()
                .Where(a => a.Profile.Id == accountId)
                .Select(a => a.Profile)
                .SingleOrDefault<Profile>();

            return profile;
        }
    }
}