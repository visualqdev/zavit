using System.Collections.Generic;
using zavit.Domain.Venues;

namespace zavit.Domain.Places.VenuePlaces
{
    public class VenuePlace
    {
        public string PlaceId { get; set; }
        public IList<Venue> Venues { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public virtual void AddVenue(Venue venue)
        {
            Venues.Add(venue);
        }
    }
}