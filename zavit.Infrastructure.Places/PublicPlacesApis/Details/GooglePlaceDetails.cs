namespace zavit.Infrastructure.Places.PublicPlacesApis.Details
{
    public class GooglePlaceDetails
    {
        public GooglePlaceDetailsGeometry geometry { get; set; }
        public string place_id { get; set; }
        public string vicinity { get; set; }
        public string name { get; set; }
    }
}