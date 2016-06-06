using System;
using System.Collections.Generic;
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

        public IEnumerable<PlaceDto> Get()
        {
            return _placeDtoService.SuggestPlaces();
        }
    }
}
