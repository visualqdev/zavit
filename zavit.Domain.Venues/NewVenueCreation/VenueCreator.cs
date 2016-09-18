using zavit.Domain.Accounts;
using zavit.Domain.Activities;

namespace zavit.Domain.Venues.NewVenueCreation
{
    public class VenueCreator : IVenueCreator
    {
        readonly IActivityRepository _activityRepository;

        public VenueCreator(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Venue Create(NewVenue newVenue, Account venueOwnerAccount)
        {
            var activities = _activityRepository.GetActivities(newVenue.ActivityIds);

            return new Venue
            {
                Name = newVenue.Name,
                OwnerAccount = venueOwnerAccount,
                Activities = activities
            };
        }
    }
}