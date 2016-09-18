using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Accounts;
using zavit.Domain.Places.Search;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Places
{
    public interface IPlaceService
    {
        Task<IEnumerable<IPlace>> Suggest(IPlaceSearchCriteria placeSearchCriteria);
        Task<Venue> AddVenue(NewVenue newVenue, string placeId, Account venueOwnerAccount);
        Task<Venue> GetDefaultVenue(string placeId);
    }
}