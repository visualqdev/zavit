namespace zavit.Infrastructure.Places
{
    public interface IGoogleApiSettings
    {
        string PlaceUri { get; }
        string ServerKey { get; }
    }
}