using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Shared;
using zavit.Domain.Venues;

namespace zavit.Domain.VenueMemberships.NewVenueMembershipCreation
{
    public class VenueMembershipCreator : IVenueMembershipCreator
    {
        readonly IVenueRepository _venueRepository;
        readonly IActivityRepository _activityRepository;
        readonly IDateTime _dateTime;

        public VenueMembershipCreator(IVenueRepository venueRepository, IActivityRepository activityRepository, IDateTime dateTime)
        {
            _venueRepository = venueRepository;
            _activityRepository = activityRepository;
            _dateTime = dateTime;
        }

        public VenueMembership Create(Account account, NewVenueMembership newVenueMembership)
        {
            var venue = _venueRepository.GetVenue(newVenueMembership.VenueId);
            var activities = _activityRepository.GetActivities(newVenueMembership.Activities);
            venue.AddActivities(activities);

            var venueMembership = new VenueMembership
            {
                Account = account,
                Venue = venue,
                Activities = activities,
                CreatedOn = _dateTime.UtcNow
            };

            return venueMembership;
        }
    }
}