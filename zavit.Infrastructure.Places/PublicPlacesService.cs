using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places
{
    public class PublicPlacesService : IPublicPlacesService
    {
        readonly IGooglePlacesApi _googlePlacesApi;
        readonly IPlaceSearchResultsTransformer _publicPlacesTransformer;
        readonly IPlaceDetailsResultTransformer _placeDetailsResultTransformer;
        public static string[] Keywords = { "sport", "gym", "tennis", "football", "suqash", "rugby", "cricket", "golf", "basketball" };

        public PublicPlacesService(IGooglePlacesApi googlePlacesApi, IPlaceSearchResultsTransformer publicPlacesTransformer, IPlaceDetailsResultTransformer placeDetailsResultTransformer)
        {
            _googlePlacesApi = googlePlacesApi;
            _publicPlacesTransformer = publicPlacesTransformer;
            _placeDetailsResultTransformer = placeDetailsResultTransformer;
        }

        public async Task<IEnumerable<PublicPlace>> GetPublicPlaces(IPlaceSearchCriteria placeSearchCriteria)
        {
            var searchResult = await _googlePlacesApi.NearbySearch(placeSearchCriteria, Keywords);
            var publicPlaces = _publicPlacesTransformer.Transform(searchResult);

            return publicPlaces;
        }

        public async Task<PublicPlace> GetPublicPlace(string placeId)
        {
            var placeDetailsResult = await _googlePlacesApi.GetDetails(placeId);
            var publicPlace = _placeDetailsResultTransformer.Transform(placeDetailsResult);

            return publicPlace;
        }
    }
}