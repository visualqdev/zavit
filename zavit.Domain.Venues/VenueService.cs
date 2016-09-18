using zavit.Domain.Accounts;
using zavit.Domain.Venues.NewVenueCreation;

namespace zavit.Domain.Venues
{
    public class VenueService : IVenueService
    {
        readonly IVenueCreator _venueCreator;

        public VenueService(IVenueCreator venueCreator)
        {
            _venueCreator = venueCreator;
        }

        public Venue CreateVenue(NewVenue newVenue, Account venueOwnerAccount)
        {
            var venue = _venueCreator.Create(newVenue, venueOwnerAccount);
            return venue;
        }
    }
}