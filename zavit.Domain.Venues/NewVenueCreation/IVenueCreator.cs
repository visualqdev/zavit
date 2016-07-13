namespace zavit.Domain.Venues.NewVenueCreation
{
    public interface IVenueCreator
    {
        Venue Create(INewVenue newVenue);
    }
}