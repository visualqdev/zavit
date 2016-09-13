namespace zavit.Domain.Places.Search
{
    public interface IPlaceSearchByNameCriteria
    {
        double Longitude { get; }
        double Latitude { get; }
        int Radius { get; }
        string Name { get; }
    }
}