using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Places.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public class PlaceSearchResultsTransformer : IPlaceSearchResultsTransformer
    {
        readonly IPlaceSearchTransformer _placeSearchTransformer;

        public PlaceSearchResultsTransformer(IPlaceSearchTransformer placeSearchTransformer)
        {
            _placeSearchTransformer = placeSearchTransformer;
        }

        public IEnumerable<PublicPlace> Transform(GooglePlaceSearchResult googlePlacesSearchResult)
        {
            var publicPlaces = googlePlacesSearchResult.results.Select(r => _placeSearchTransformer.Transform(r));
            return publicPlaces;
        }
    }
}