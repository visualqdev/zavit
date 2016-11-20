using zavit.Domain.Activities;
using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.DefaultVenues
{
    public class DefaultVenueProvider : IDefaultVenueProvider
    {
        readonly IActivityRepository _activityRepository;

        public DefaultVenueProvider(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public Venue ProvideDefaultVenue(PublicPlace publicPlace)
        {
            var defaultActivities = _activityRepository.GetDefaultActivities();

            return new Venue
            {
                Name = publicPlace.Name,
                Activities = defaultActivities,
                Address = publicPlace.Address,
                PublicPlaceId = publicPlace.PlaceId,
                Latitude = publicPlace.Latitude,
                Longitude = publicPlace.Longitude
            };
        }
    }
}