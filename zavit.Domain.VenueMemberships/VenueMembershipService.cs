using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;

namespace zavit.Domain.VenueMemberships
{
    public class VenueMembershipService : IVenueMembershipService
    {
        readonly IVenueMembershipCreator _venueMembershipCreator;
        readonly IVenueMembershipRepository _venueMembershipRepository;

        public VenueMembershipService(IVenueMembershipCreator venueMembershipCreator, IVenueMembershipRepository venueMembershipRepository)
        {
            _venueMembershipCreator = venueMembershipCreator;
            _venueMembershipRepository = venueMembershipRepository;
        }

        public VenueMembership AddUserToVenue(Account account, NewVenueMembership newVenueMembership)
        {
            var venueMembership = _venueMembershipRepository.GetMembership(account.Id, newVenueMembership.VenueId);

            if (venueMembership != null) return venueMembership;

            venueMembership = _venueMembershipCreator.Create(account, newVenueMembership);

            _venueMembershipRepository.Save(venueMembership);
            return venueMembership;
        }

        public IEnumerable<VenueMembership> GetVenueMemberships(Account account)
        {
            var venueMemberships = _venueMembershipRepository.GetMemberships(account.Id);
            return venueMemberships;
        }
    }
}