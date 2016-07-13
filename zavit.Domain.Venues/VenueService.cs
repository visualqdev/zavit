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

        public Venue CreateVenue(INewVenue newVenue)
        {
            var venue = _venueCreator.Create(newVenue);
            return venue;
        }
    }
}