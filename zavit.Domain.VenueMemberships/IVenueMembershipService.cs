using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;

namespace zavit.Domain.VenueMemberships
{
    public interface IVenueMembershipService
    {
        VenueMembership AddUserToVenue(Account account, NewVenueMembership newVenueMembership);
        IEnumerable<VenueMembership> GetVenueMemberships(Account account);
    }
}