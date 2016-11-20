using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Domain.Venues.Search;
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
        public static string[] Keywords = { "gym", "tennis", "football", "squash", "rugby", "cricket", "golf", "basketball", "badminton", "swimming" };

        public PublicPlacesService(IGooglePlacesApi googlePlacesApi, IPlaceSearchResultsTransformer publicPlacesTransformer, IPlaceDetailsResultTransformer placeDetailsResultTransformer)
        {
            _googlePlacesApi = googlePlacesApi;
            _publicPlacesTransformer = publicPlacesTransformer;
            _placeDetailsResultTransformer = placeDetailsResultTransformer;
        }

        public async Task<IEnumerable<PublicPlace>> GetPublicPlaces(IVenueSearchCriteria venueSearchCriteria)
        {

            var searchResult = !string.IsNullOrEmpty(venueSearchCriteria.Name)
                ? await _googlePlacesApi.NearbySearchByName(venueSearchCriteria, Keywords)
                : await _googlePlacesApi.NearbySearch(venueSearchCriteria, Keywords);

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