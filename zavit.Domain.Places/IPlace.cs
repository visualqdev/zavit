namespace zavit.Domain.Places
{
    public interface IPlace
    {
        double Longitude { get; }
        double Latitude { get; }
        string PlaceId { get; }
    }
}