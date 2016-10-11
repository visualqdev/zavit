using System.Collections.Generic;
using zavit.Domain.Shared.ResultCollections;

namespace zavit.Domain.VenueMemberships
{
    public interface IVenueMembershipRepository
    {
        void Save(VenueMembership venueMembership);
        IEnumerable<VenueMembership> GetMemberships(int accountId);
        IResultCollection<VenueMembership> GetMemberships(int venueId, int skip, int take, int? excludeAccountId = null);
        VenueMembership GetMembership(int accountId, int venueId);
        void Update(VenueMembership venueMembership);
    }
}