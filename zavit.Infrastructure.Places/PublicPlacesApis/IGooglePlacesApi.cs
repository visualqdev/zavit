using System.Threading.Tasks;
using zavit.Domain.Places.Search;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public interface IGooglePlacesApi
    {
        Task<GooglePlaceSearchResult> NearbySearch(IPlaceSearchCriteria placeSearchCriteria);
        Task<GooglePlaceDetailsResult> GetDetails(string placeId);
    }
}