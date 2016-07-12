using zavit.Domain.Places;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoServices.Venues
{
    public class VenueDtoService : IVenueDtoService
    {
        readonly IPlaceService _placeService;
        readonly IVenueDtoFactory _venueDtoFactory;

        public VenueDtoService(IPlaceService placeService, IVenueDtoFactory venueDtoFactory)
        {
            _placeService = placeService;
            _venueDtoFactory = venueDtoFactory;
        }

        public VenueDto AddVenue(VenueDto venueDto, string placeId)
        {
            var venue = _placeService.AddVenue(venueDto, placeId);
            var newVenueDto = _venueDtoFactory.Create(venue);
            return newVenueDto;
        }
    }
}