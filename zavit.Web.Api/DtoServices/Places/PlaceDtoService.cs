using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Places;
using zavit.Web.Api.DtoFactories.Places;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.DtoServices.Places
{
    public class PlaceDtoService : IPlaceDtoService
    {
        readonly IPlaceService _placeService;
        readonly IPlaceDtoFactory _placeDtoFactory;

        public PlaceDtoService(IPlaceService placeService, IPlaceDtoFactory placeDtoFactory)
        {
            _placeService = placeService;
            _placeDtoFactory = placeDtoFactory;
        }

        public IEnumerable<PlaceDto> SuggestPlaces()
        {
            var places = _placeService.Suggest();
            var placeDtos = places.Select(p => _placeDtoFactory.CreateItem(p));
            return placeDtos;
        }
    }
}