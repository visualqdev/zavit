namespace zavit.Infrastructure.Places
{
    public interface IGoogleApiSearchSettings
    {
        string PlaceSearchUri { get; }
        string ServerKey { get; }
    }
}