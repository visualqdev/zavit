namespace zavit.Domain.Places.VenuePlaces
{
    public interface IVenuePlaceRepository
    {
        void Save(VenuePlace place);
        void Update(VenuePlace place);
        VenuePlace Get(string placeId);
    }
}