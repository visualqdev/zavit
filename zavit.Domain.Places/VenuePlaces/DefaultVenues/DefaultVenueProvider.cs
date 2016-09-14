using zavit.Domain.Activities;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Venues;

namespace zavit.Domain.Places.VenuePlaces.DefaultVenues
{
    public class DefaultVenueProvider : IDefaultVenueProvider
    {
        readonly IActivityRepository _activityRepository;

        public DefaultVenueProvider(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Venue ProvideDefaultVenue(VenuePlace venuePlace)
        {
            return CreateVenue(venuePlace.Name);
        }

        public Venue ProvideDefaultVenue(PublicPlace publicPlace)
        {
            return CreateVenue(publicPlace.Name);
        }

        Venue CreateVenue(string venueName)
        {
            var defaultActivities = _activityRepository.GetDefaultActivities();

            return new Venue
            {
                Name = venueName,
                Activities = defaultActivities
            };
        }
    }
}