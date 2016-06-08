using zavit.Domain.Places.Search;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public interface IGooglePlaceSearchApi
    {
        GooglePlacesSearchResult NearbySearch(IPlaceSearchCriteria placeSearchCriteria);
    }
}