using System.Collections.Generic;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Venues;

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
        public IList<Venue> Venues => new List<Venue>();
    }
}