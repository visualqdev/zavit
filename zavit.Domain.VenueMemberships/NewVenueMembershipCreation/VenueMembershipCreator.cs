using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Venues;

namespace zavit.Domain.VenueMemberships.NewVenueMembershipCreation
{
    public class VenueMembershipCreator : IVenueMembershipCreator
    {
        readonly IVenueRepository _venueRepository;
        readonly IActivityRepository _activityRepository;

        public VenueMembershipCreator(IVenueRepository venueRepository, IActivityRepository activityRepository)
        {
            _venueRepository = venueRepository;
            _activityRepository = activityRepository;
        }

        public VenueMembership Create(Account account, NewVenueMembership newVenueMembership)
        {
            var venue = _venueRepository.GetVenue(newVenueMembership.VenueId);
            var activities = _activityRepository.GetActivities(newVenueMembership.Activities);

            var venueMembership = new VenueMembership
            {
                Account = account,
                Venue = venue,
                Activities = activities
            };

            return venueMembership;
        }
    }
}