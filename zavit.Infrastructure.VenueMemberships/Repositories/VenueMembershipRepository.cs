using System.Collections.Generic;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Transform;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Shared.ResultCollections;
using zavit.Domain.VenueMemberships;
using zavit.Domain.Venues;
using zavit.Infrastructure.Core.ResultCollections;

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

        public IResultCollection<VenueMembership> GetMemberships(int venueId, int skip, int take, int? excludeAccountId = null)
        {
            Account accountAlias = null;
            Activity activityAlias = null;

            var venueMembershipsQuery = _session.QueryOver<VenueMembership>()
                .JoinAlias(m => m.Account, () => accountAlias, JoinType.InnerJoin)
                .JoinAlias(m => m.Activities, () => activityAlias, JoinType.LeftOuterJoin)
                .Fetch(m => m.Activities).Eager
                .Fetch(m => m.Account).Eager
                .Fetch(m => m.Account.Profile).Eager
                .Where(m => m.Venue.Id == venueId);

            if (excludeAccountId.HasValue)
            {
                venueMembershipsQuery.Where(m => m.Account.Id != excludeAccountId.Value);
            }

            var venueMemberships = venueMembershipsQuery
                .OrderBy(m => m.CreatedOn).Desc
                .TransformUsing(Transformers.DistinctRootEntity)
                .Skip(skip)
                .Take(take + 1)
                .List();

            return new ResultCollection<VenueMembership>(venueMemberships, take);
        }

        public VenueMembership GetMembership(int accountId, int venueId)
        {
            return _session.QueryOver<VenueMembership>()
                .Where(m => m.Account.Id == accountId && m.Venue.Id == venueId)
                .SingleOrDefault();
        }

        public VenueMembership GetMembership(int accountId, string publicPlaceId)
        {
            Venue venueAias = null;

            return _session.QueryOver<VenueMembership>()
                .Fetch(l => l.Venue).Eager
                .JoinAlias(m => m.Venue, () => venueAias, JoinType.InnerJoin)
                .Where(m => m.Account.Id == accountId)
                .And(() => venueAias.PublicPlaceId == publicPlaceId)
                .SingleOrDefault();
        }

        public void Update(VenueMembership venueMembership)
        {
            _session.Update(venueMembership);
            _session.Flush();
        }
    }
}