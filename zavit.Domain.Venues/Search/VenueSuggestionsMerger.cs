using System;
using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.Search
{
    public class VenueSuggestionsMerger : IVenueSuggestionsMerger
    {
        readonly IPublicVenueSuggestionFactory _publicVenueSuggestionFactory;

        public VenueSuggestionsMerger(IPublicVenueSuggestionFactory publicVenueSuggestionFactory)
        {
            _publicVenueSuggestionFactory = publicVenueSuggestionFactory;
        }

        public IEnumerable<IVenue> Merge(IEnumerable<PublicPlace> publicPlaces, IEnumerable<Venue> venues)
        {
            var venueSuggestions = new List<IVenue>();
            var publicPlaceIds = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            foreach (var venue in venues)
            {
                venueSuggestions.Add(venue);
                publicPlaceIds.Add(venue.PublicPlaceId);
            }

            venueSuggestions.AddRange(
                publicPlaces
                    .Where(p => !publicPlaceIds.Contains(p.PlaceId))
                    .Select(p => _publicVenueSuggestionFactory.Create(p)));

            return venueSuggestions;
        }
    }
}