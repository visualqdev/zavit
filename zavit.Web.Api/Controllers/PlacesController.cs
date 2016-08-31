using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using zavit.Web.Api.Dtos.Places;
using zavit.Web.Api.DtoServices.Places;

namespace zavit.Web.Api.Controllers
{
    public class PlacesController : ApiController
    {
        readonly IPlaceDtoService _placeDtoService;

        public PlacesController(IPlaceDtoService placeDtoService)
        {
            _placeDtoService = placeDtoService;
        }

        public async Task<IEnumerable<PlaceDto>> Get([FromUri] PlaceSearchCriteriaDto placeSearchCriteriaDto)
        {
            return await _placeDtoService.SuggestPlaces(placeSearchCriteriaDto);
        }
    }
}
