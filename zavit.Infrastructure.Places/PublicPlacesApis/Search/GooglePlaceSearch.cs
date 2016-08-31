namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public class GooglePlaceSearch
    {
        public GooglePlaceSearchGeometry geometry { get; set; }
        public string place_id { get; set; }
        public string vicinity { get; set; }
        public string name { get; set; }
    }
}