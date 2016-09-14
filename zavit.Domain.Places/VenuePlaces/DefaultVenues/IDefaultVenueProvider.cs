using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Venues;

namespace zavit.Domain.Places.VenuePlaces.DefaultVenues
{
    public interface IDefaultVenueProvider
    {
        Venue ProvideDefaultVenue(VenuePlace venuePlace);
        Venue ProvideDefaultVenue(PublicPlace publicPlace);
    }
}