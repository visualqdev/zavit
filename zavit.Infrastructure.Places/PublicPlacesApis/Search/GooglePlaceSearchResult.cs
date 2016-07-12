using System.Collections.Generic;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public class GooglePlaceSearchResult
    {
         public IEnumerable<GooglePlaceSearch> results { get; set; }
    }
}