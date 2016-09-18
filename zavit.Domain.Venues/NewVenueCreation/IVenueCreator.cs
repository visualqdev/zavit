using zavit.Domain.Accounts;

namespace zavit.Domain.Venues.NewVenueCreation
{
    public interface IVenueCreator
    {
        Venue Create(NewVenue newVenue, Account venueOwnerAccount);
    }
}