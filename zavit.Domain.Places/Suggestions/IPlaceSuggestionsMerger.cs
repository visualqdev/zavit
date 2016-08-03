using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.VenuePlaces;

namespace zavit.Domain.Places.Suggestions
{
    public interface IPlaceSuggestionsMerger
    {
        IEnumerable<IPlace> Merge(IEnumerable<PublicPlace> publicPlaces, IEnumerable<VenuePlace> venuePlaces);
    }
}