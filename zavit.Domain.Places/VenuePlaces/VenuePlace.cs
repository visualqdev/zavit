using System.Collections.Generic;
using zavit.Domain.Shared;
using zavit.Domain.Venues;

namespace zavit.Domain.Places.VenuePlaces
{
    public class VenuePlace : IEntity<int>, IPlace
    {
        public virtual int Id { get; set; }
        public virtual string PlaceId { get; set; }
        public virtual IList<Venue> Venues { get; set; }
        public virtual string Address { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual string Name { get; set; }


        public virtual void AddVenue(Venue venue)
        {
            venue.Address = Address;
            Venues.Add(venue);
        }
    }
}