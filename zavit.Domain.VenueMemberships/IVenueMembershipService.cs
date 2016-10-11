using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Shared.ResultCollections;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;

namespace zavit.Domain.VenueMemberships
{
    public interface IVenueMembershipService
    {
        VenueMembership AddUserToVenue(Account account, NewVenueMembership newVenueMembership);
        IEnumerable<VenueMembership> GetVenueMembershipsForUser(Account account);
        VenueMembership GetVenueMembership(Account account, int venueId);
        IResultCollection<VenueMembership> GetAllVenueMemberships(int venueId, int skip, int take, Account excludeAccount = null);
    }
}