using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.Search
{
    public class PublicVenueSuggestion : IVenue
    {
        public PublicPlace PublicPlace { get; set; }

        public int Id { get; }

        public double Longitude => PublicPlace.Longitude;
        public double Latitude => PublicPlace.Latitude;
        public string PublicPlaceId => PublicPlace.PlaceId;
        
        public string Name => PublicPlace.Name;
        public string Address => PublicPlace.Address;
    }
}