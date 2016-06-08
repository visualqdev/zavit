using System.Collections.Generic;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.DtoServices.Places
{
    public interface IPlaceDtoService
    {
        IEnumerable<PlaceDto> SuggestPlaces(PlaceSearchCriteriaDto placeSearchCriteriaDto);
    }
}