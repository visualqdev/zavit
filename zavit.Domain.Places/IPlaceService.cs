using System.Collections.Generic;
using zavit.Domain.Places.Search;

namespace zavit.Domain.Places
{
    public interface IPlaceService
    {
        IEnumerable<IPlace> Suggest(IPlaceSearchCriteria placeSearchCriteria);
    }
}