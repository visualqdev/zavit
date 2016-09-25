using System.Collections.Generic;

namespace zavit.Domain.VenueMemberships
{
    public interface IVenueMembershipRepository
    {
        void Save(VenueMembership venueMembership);
        IEnumerable<VenueMembership> GetMemberships(int accountId);
    }
}