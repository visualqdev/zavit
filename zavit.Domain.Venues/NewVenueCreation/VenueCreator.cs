using zavit.Domain.Accounts;

namespace zavit.Domain.Venues.NewVenueCreation
{
    public class VenueCreator : IVenueCreator
    {
        public Venue Create(INewVenue newVenue, Account venueOwnerAccount)
        {
            return new Venue
            {
                Name = newVenue.Name,
                Account = venueOwnerAccount
            };
        }
    }
}