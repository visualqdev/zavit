using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Domain.Places.VenuePlaces;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Places
{
    public class PlaceService : IPlaceService
    {
        readonly IPublicPlacesService _publicPlacesService;
        readonly IVenuePlaceRepository _venuePlaceRepository;
        readonly IVenueService _venueService;
        readonly IVenuePlaceCreator _venuePlaceCreator;

        public PlaceService(IPublicPlacesService publicPlacesService, IVenuePlaceRepository venuePlaceRepository, IVenueService venueService, IVenuePlaceCreator venuePlaceCreator)
        {
            _publicPlacesService = publicPlacesService;
            _venuePlaceRepository = venuePlaceRepository;
            _venueService = venueService;
            _venuePlaceCreator = venuePlaceCreator;
        }

        public IEnumerable<IPlace> Suggest(IPlaceSearchCriteria placeSearchCriteria)
        {
            var publicPlaces = _publicPlacesService.GetPublicPlaces(placeSearchCriteria);
            return publicPlaces;
        }

        public Venue AddVenue(INewVenue newVenue, string placeId)
        {
            var place = _venuePlaceRepository.Get(placeId);

            var isNewPlace = false;
            if (place == null)
            {
                isNewPlace = true;
                place =_venuePlaceCreator.Create(placeId);
            }

            var venue = _venueService.CreateVenue(newVenue);
            place.AddVenue(venue);

            if (isNewPlace)
                _venuePlaceRepository.Save(place);
            else
                _venuePlaceRepository.Update(place);

            return venue;
        }
    }
}