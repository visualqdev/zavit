using System.Collections.Generic;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public class GooglePlacesSearchResult
    {
         public IEnumerable<GooglePlace> results { get; set; }
    }

    public class GooglePlace
    {
        public GoogleGeometry geometry { get; set; }
        public string place_id { get; set; }
        public string vicinity { get; set; }
        public string name { get; set; }
    }

    public class GoogleGeometry
    {
        public GoogleLocation location { get; set; }
    }

    public class GoogleLocation
    {
        public double lat { get; set; }
        public double lng { get; set; }
    }
}