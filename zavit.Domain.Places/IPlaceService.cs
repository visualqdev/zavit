using System.Collections.Generic;
using zavit.Domain.Places.Search;
using zavit.Domain.Venues;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Places
{
    public interface IPlaceService
    {
        IEnumerable<IPlace> Suggest(IPlaceSearchCriteria placeSearchCriteria);
        Venue AddVenue(INewVenue newVenue, string placeId);
    }
}