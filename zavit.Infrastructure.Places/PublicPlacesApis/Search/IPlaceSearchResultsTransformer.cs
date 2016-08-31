using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public interface IPlaceSearchResultsTransformer
    {
        IEnumerable<PublicPlace> Transform(GooglePlaceSearchResult googlePlacesSearchResult);
    }
}