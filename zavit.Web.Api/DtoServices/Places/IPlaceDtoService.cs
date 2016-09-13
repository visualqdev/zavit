using System.Collections.Generic;
using System.Threading.Tasks;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.DtoServices.Places
{
    public interface IPlaceDtoService
    {
        Task<IEnumerable<PlaceDto>> SuggestPlaces(PlaceSearchCriteriaDto placeSearchCriteriaDto);
        Task<IEnumerable<PlaceDto>> SuggestPlacesByName(PlaceSearchByNameCriteriaDto placeSearchCriteriaDto);
    }
}