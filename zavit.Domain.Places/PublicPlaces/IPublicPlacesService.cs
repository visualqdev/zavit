using System.Collections.Generic;
using zavit.Domain.Places.Search;

namespace zavit.Domain.Places.PublicPlaces
{
    public interface IPublicPlacesService
    {
        IEnumerable<PublicPlace> GetPublicPlaces(IPlaceSearchCriteria placeSearchCriteria);
    }
}