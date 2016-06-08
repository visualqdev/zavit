using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;

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
    }
}