using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Domain.Places.Search;

namespace zavit.Domain.Places.VenuePlaces
{
    public interface IVenuePlaceRepository
    {
        void Save(VenuePlace place);
        void Update(VenuePlace place);
        VenuePlace Get(string placeId);
        Task<IEnumerable<VenuePlace>> SearchPlaces(IPlaceSearchCriteria placeSearchCriteria);
    }
}