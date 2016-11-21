namespace zavit.Domain.Venues.Search
{
    public interface IVenueSearchCriteria
    {
        double Longitude { get; }
        double Latitude { get; }
        int Radius { get; }
        string Name { get; }
    }
}