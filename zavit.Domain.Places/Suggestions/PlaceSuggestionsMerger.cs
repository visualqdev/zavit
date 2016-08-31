using System;
using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.VenuePlaces;

namespace zavit.Domain.Places.Suggestions
{
    public class PlaceSuggestionsMerger : IPlaceSuggestionsMerger
    {
        readonly IPublicPlaceSuggestionFactory _publicPlaceSuggestionFactory;

        public PlaceSuggestionsMerger(IPublicPlaceSuggestionFactory publicPlaceSuggestionFactory)
        {
            _publicPlaceSuggestionFactory = publicPlaceSuggestionFactory;
        }

        public IEnumerable<IPlace> Merge(IEnumerable<PublicPlace> publicPlaces, IEnumerable<VenuePlace> venuePlaces)
        {
            var placeSuggestions = new List<IPlace>();
            var venuePlaceIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var venuePlace in venuePlaces)
            {
                placeSuggestions.Add(venuePlace);
                venuePlaceIds.Add(venuePlace.PlaceId);
            }

            placeSuggestions.AddRange(
                publicPlaces
                    .Where(p => !venuePlaceIds.Contains(p.PlaceId))
                    .Select(p => _publicPlaceSuggestionFactory.Create(p)));

            return placeSuggestions;
        }
    }
}