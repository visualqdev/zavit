using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Places
{
    public class PlaceService : IPlaceService
    {
        readonly IPublicPlacesService _publicPlacesService;

        public PlaceService(IPublicPlacesService publicPlacesService)
        {
            _publicPlacesService = publicPlacesService;
        }

        public IEnumerable<IPlace> Suggest(IPlaceSearchCriteria placeSearchCriteria)
        {
            var publicPlaces = _publicPlacesService.GetPublicPlaces(placeSearchCriteria);
            return publicPlaces;
        }

        public Venue AddVenue(INewVenue newVenue, string placeId)
        {
            throw new System.NotImplementedException();
        }
    }
}