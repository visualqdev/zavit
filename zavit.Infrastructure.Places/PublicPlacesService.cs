using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis;

namespace zavit.Infrastructure.Places
{
    public class PublicPlacesService : IPublicPlacesService
    {
        readonly IGooglePlaceSearchApi _googlePlaceSearchApi;
        readonly IPublicPlacesTransformer _publicPlacesTransformer;

        public PublicPlacesService(IGooglePlaceSearchApi googlePlaceSearchApi, IPublicPlacesTransformer publicPlacesTransformer)
        {
            _googlePlaceSearchApi = googlePlaceSearchApi;
            _publicPlacesTransformer = publicPlacesTransformer;
        }

        public IEnumerable<PublicPlace> GetPublicPlaces(IPlaceSearchCriteria placeSearchCriteria)
        {
            var searchResult = _googlePlaceSearchApi.NearbySearch(placeSearchCriteria);
            var publicPlaces = _publicPlacesTransformer.Transform(searchResult);

            return publicPlaces;
        }
    }
}