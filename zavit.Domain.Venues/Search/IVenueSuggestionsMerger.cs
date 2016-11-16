using System.Collections.Generic;
using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.Search
{
    public interface IVenueSuggestionsMerger
    {
        IEnumerable<IVenue> Merge(IEnumerable<PublicPlace> publicPlaces, IEnumerable<Venue> venues);
    }
}