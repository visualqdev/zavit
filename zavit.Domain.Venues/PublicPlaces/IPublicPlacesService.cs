using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Venues.Search;

namespace zavit.Domain.Venues.PublicPlaces
{
    public interface IPublicPlacesService
    {
        Task<IEnumerable<PublicPlace>> GetPublicPlaces(IVenueSearchCriteria placeSearchCriteria);
        Task<PublicPlace> GetPublicPlace(string placeId);
    }
}   