using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Venues.Search;

namespace zavit.Domain.Venues
{
    public interface IVenueRepository
    {
        Venue GetVenue(int id);
        Venue GetVenue(string publicPlaceId);
        Task<IEnumerable<Venue>> SearchVenues(IVenueSearchCriteria venueSearchCriteria);
        void Save(Venue venue);
    }
}