using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public interface IGooglePlacesApi
    {
        Task<GooglePlaceSearchResult> NearbySearch(IPlaceSearchCriteria placeSearchCriteria, IEnumerable<string> keywords);
        Task<GooglePlaceDetailsResult> GetDetails(string placeId);
        Task<GooglePlaceSearchResult> NearbySearchByName(IPlaceSearchByNameCriteria placeSearchByNameCriteria);
    }   
}