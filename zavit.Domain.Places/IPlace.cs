using System.Collections.Generic;
using zavit.Domain.Venues;

namespace zavit.Domain.Places
{
    public interface IPlace
    {
        double Longitude { get; }
        double Latitude { get; }
        string PlaceId { get; }
        string Name { get; }
        string Address { get; }
        IList<Venue> Venues { get; }
    }
}