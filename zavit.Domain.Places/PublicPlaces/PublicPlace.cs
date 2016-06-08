namespace zavit.Domain.Places.PublicPlaces
{
    public class PublicPlace : IPlace
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string PlaceId { get; set; }
    }
}