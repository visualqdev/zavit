using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;

namespace zavit.Domain.VenueMemberships
{
    public class VenueMembershipService : IVenueMembershipService
    {
        readonly IVenueMembershipCreator _venueMembershipCreator;
        readonly IVenueMembershipRepository _venueMembershipRepository;
        readonly IActivityRepository _activityRepository;

        public VenueMembershipService(IVenueMembershipCreator venueMembershipCreator, IVenueMembershipRepository venueMembershipRepository, IActivityRepository activityRepository)
        {
            _venueMembershipCreator = venueMembershipCreator;
            _venueMembershipRepository = venueMembershipRepository;
            _activityRepository = activityRepository;
        }

        public VenueMembership AddUserToVenue(Account account, NewVenueMembership newVenueMembership)
        {
            var venueMembership = _venueMembershipRepository.GetMembership(account.Id, newVenueMembership.VenueId);

            if (venueMembership != null)
            {
                var activities = _activityRepository.GetActivities(newVenueMembership.Activities);
                if (venueMembership.UpdateActivities(activities))
                    _venueMembershipRepository.Update(venueMembership);
                return venueMembership;
            }

            venueMembership = _venueMembershipCreator.Create(account, newVenueMembership);

            _venueMembershipRepository.Save(venueMembership);
            return venueMembership;
        }

        public IEnumerable<VenueMembership> GetVenueMemberships(Account account)
        {
            var venueMemberships = _venueMembershipRepository.GetMemberships(account.Id);
            return venueMemberships;
        }

        public VenueMembership GetVenueMembership(Account account, int venueId)
        {
            var venueMembership = _venueMembershipRepository.GetMembership(account.Id, venueId);
            return venueMembership;
        }
    }
}