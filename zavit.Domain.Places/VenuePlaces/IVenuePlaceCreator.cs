using System.Threading.Tasks;

namespace zavit.Domain.Places.VenuePlaces
{
    public interface IVenuePlaceCreator
    {
        Task<VenuePlace> Create(string placeId);
    }
}