namespace zavit.Domain.Venues
{
    public interface IVenueRepository
    {
        Venue GetVenue(int id);
    }
}