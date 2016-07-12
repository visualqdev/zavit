using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Places.Search;

namespace zavit.Domain.Places.PublicPlaces
{
    public interface IPublicPlacesService
    {
        Task<IEnumerable<PublicPlace>> GetPublicPlaces(IPlaceSearchCriteria placeSearchCriteria);
        Task<PublicPlace> GetPublicPlace(string placeId);
    }
}