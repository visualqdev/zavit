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
    }
}