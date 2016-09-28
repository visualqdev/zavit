using System.Collections.Generic;
using NHibernate;
using zavit.Domain.VenueMemberships;

namespace zavit.Infrastructure.VenueMemberships.Repositories
{
    public class VenueMembershipRepository : IVenueMembershipRepository
    {
        readonly ISession _session;

        public VenueMembershipRepository(ISession session)
        {
            _session = session;
        }

        public void Save(VenueMembership venueMembership)
        {
            _session.Save(venueMembership);
            _session.Flush();
        }

        public IEnumerable<VenueMembership> GetMemberships(int accountId)
        {
            return _session.QueryOver<VenueMembership>()
                .Where(m => m.Account.Id == accountId)
                .OrderBy(m => m.CreatedOn).Desc
                .List();
        }
    }
}