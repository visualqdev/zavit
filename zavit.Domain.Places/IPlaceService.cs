using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Places.Search;
using zavit.Domain.Venues;

namespace zavit.Domain.Places
{
    public interface IPlaceService
    {
        Task<IEnumerable<IPlace>> Suggest(IPlaceSearchCriteria placeSearchCriteria);
        Task<Venue> GetDefaultVenue(string placeId);
    }
}