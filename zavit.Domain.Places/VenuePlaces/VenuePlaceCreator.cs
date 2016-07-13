using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Venues;

namespace zavit.Domain.Places.VenuePlaces
{
    public class VenuePlaceCreator : IVenuePlaceCreator
    {
        readonly IPublicPlacesService _publicPlacesService;

        public VenuePlaceCreator(IPublicPlacesService publicPlacesService)
        {
            _publicPlacesService = publicPlacesService;
        }

        public VenuePlace Create(string placeId)
        {
            var publicPlace = _publicPlacesService.GetPublicPlace(placeId);
            var venuePlace = new VenuePlace
            {
                Venues = new List<Venue>(),
                PlaceId = publicPlace.PlaceId,
                Address = publicPlace.Address,
                Latitude = publicPlace.Latitude,
                Longitude = publicPlace.Longitude
            };

            return venuePlace;
        }
    }
}