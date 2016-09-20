namespace zavit.Domain.Places.Search
{
    public interface IPlaceSearchCriteria
    {
        double Longitude { get; }
        double Latitude { get; }
        int Radius { get; }
        string Name { get; }
    }
}