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
            if (venuePlace.Venues.Count > 0)
                return venuePlace.Venues[0];

            return CreateVenue(venuePlace.Name, venuePlace.Address);
        }

        public Venue ProvideDefaultVenue(PublicPlace publicPlace)
        {
            return CreateVenue(publicPlace.Name, publicPlace.Address);
        }

        Venue CreateVenue(string venueName, string venueAddress)
        {
            var defaultActivities = _activityRepository.GetDefaultActivities();

            return new Venue
            {
                Name = venueName,
                Activities = defaultActivities,
                Address = venueAddress
            };
        }
    }
}