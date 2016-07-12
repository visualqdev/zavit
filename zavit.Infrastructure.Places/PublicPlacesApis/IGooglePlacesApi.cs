using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public interface IGooglePlacesApi
    {
        GooglePlaceSearchResult NearbySearch(IPlaceSearchCriteria placeSearchCriteria);
        GooglePlaceDetailsResult GetDetails(string placeId);
    }
}