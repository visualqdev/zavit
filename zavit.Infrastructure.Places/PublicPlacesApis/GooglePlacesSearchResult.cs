using System.Collections.Generic;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public class GooglePlacesSearchResult
    {
         public IEnumerable<GooglePlace> result { get; set; }
    }

    public class GooglePlace
    {
        public string place_id { get; set; }
    }
}