using System.Collections.Generic;
using zavit.Domain.Activities;
using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.Search
{
    public class PublicVenueSuggestion : IVenue
    {
        public PublicVenueSuggestion()
        {
            Activities = new List<Activity>();
        }

        public PublicPlace PublicPlace { get; set; }

        public int Id { get; }

        public double Longitude => PublicPlace.Longitude;
        public double Latitude => PublicPlace.Latitude;
        public string PublicPlaceId => PublicPlace.PlaceId;
        
        public string Name => PublicPlace.Name;
        public string Address => PublicPlace.Address;
        public IList<Activity> Activities { get; }
    }
}