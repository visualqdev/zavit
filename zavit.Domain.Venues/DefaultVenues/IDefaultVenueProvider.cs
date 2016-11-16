using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Domain.Venues.DefaultVenues
{
    public interface IDefaultVenueProvider
    {
        Venue ProvideDefaultVenue(PublicPlace publicPlace);
    }
}