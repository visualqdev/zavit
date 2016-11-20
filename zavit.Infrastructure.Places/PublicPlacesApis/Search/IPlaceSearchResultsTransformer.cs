using System.Collections.Generic;
using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public interface IPlaceSearchResultsTransformer
    {
        IEnumerable<PublicPlace> Transform(GooglePlaceSearchResult googlePlacesSearchResult);
    }
}