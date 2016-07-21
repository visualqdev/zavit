using zavit.Domain.Places.PublicPlaces;

namespace zavit.Domain.Places.Suggestions
{
    public class PublicPlaceSuggestion : IPlace
    {
        public PublicPlace PublicPlace { get; set; }

        public double Longitude => PublicPlace.Longitude;
        public double Latitude => PublicPlace.Latitude;
        public string PlaceId => PublicPlace.PlaceId;
        public string Name => PublicPlace.Name;
        public string Address => PublicPlace.Address;
    }
}