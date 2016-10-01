using System.Collections.Generic;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using zavit.Domain.Activities;
using zavit.Domain.VenueMemberships;
using zavit.Domain.Venues;

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
            Venue venueAlias = null;
            Activity activityAlias = null;

            return _session.QueryOver<VenueMembership>()
                .JoinAlias(m => m.Venue, () => venueAlias, JoinType.InnerJoin)
                .JoinAlias(m => m.Activities, () => activityAlias, JoinType.LeftOuterJoin)
                .Where(m => m.Account.Id == accountId)
                .Fetch(m => m.Venue).Eager
                .Fetch(m => m.Activities).Eager
                .OrderBy(m => m.CreatedOn).Desc
                .TransformUsing(Transformers.DistinctRootEntity)
                .List();
        }

        public VenueMembership GetMembership(int accountId, int venueId)
        {
            return _session.QueryOver<VenueMembership>()
                .Where(m => m.Account.Id == accountId && m.Venue.Id == venueId)
                .SingleOrDefault();
        }
    }
}