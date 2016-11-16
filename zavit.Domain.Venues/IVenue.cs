using System.Collections.Generic;
using zavit.Domain.Activities;

namespace zavit.Domain.Venues
{
    public interface IVenue
    {
        int Id { get; }
        string Name { get; }
        double Longitude { get; }
        double Latitude { get; }
        string PublicPlaceId { get; }
        string Address { get; }
        IList<Activity> Activities { get; }
    }
}