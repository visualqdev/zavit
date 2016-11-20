using System.Threading.Tasks;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.NewVenueCreation
{
    public class VenueCreator : IVenueCreator
    {
        readonly IActivityRepository _activityRepository;
        readonly IPublicPlacesService _publicPlacesService;

        public VenueCreator(IActivityRepository activityRepository, IPublicPlacesService publicPlacesService)
        {
            _activityRepository = activityRepository;
            _publicPlacesService = publicPlacesService;
        }

        public async Task<Venue> Create(NewVenue newVenue, Account venueOwnerAccount)
        {
            var publicPlace = await _publicPlacesService.GetPublicPlace(newVenue.PublicPlaceId);

            var activities = _activityRepository.GetActivities(newVenue.ActivityIds);

            return new Venue
            {
                Name = string.IsNullOrWhiteSpace(newVenue.Name) ? publicPlace.Name : newVenue.Name,
                OwnerAccount = venueOwnerAccount,
                Activities = activities,
                PublicPlaceId = publicPlace.PlaceId,
                Address = publicPlace.Address,
                Latitude = publicPlace.Latitude,
                Longitude = publicPlace.Longitude
            };
        }
    }
}