namespace zavit.Domain.Venues.NewVenueCreation
{
    public class VenueCreator : IVenueCreator
    {
        public Venue Create(INewVenue newVenue)
        {
            return new Venue
            {
                Name = newVenue.Name
            };
        }
    }
}