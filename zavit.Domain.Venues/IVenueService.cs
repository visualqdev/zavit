using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Accounts;
using zavit.Domain.Venues.NewVenueCreation;
using zavit.Domain.Venues.Search;

namespace zavit.Domain.Venues
{
    public interface IVenueService
    {
        Task<Venue> CreateVenue(NewVenue newVenue, Account venueOwnerAccount);
        Task<IEnumerable<IVenue>> SuggestVenues(IVenueSearchCriteria venueSearchCriteria);
        Task<Venue> GetDefaultVenue(string publicPlaceId);
    }
}