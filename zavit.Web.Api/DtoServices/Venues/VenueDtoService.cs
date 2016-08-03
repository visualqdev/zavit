using System.Threading.Tasks;
using zavit.Domain.Places;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Venues
{
    public class VenueDtoService : IVenueDtoService
    {
        readonly IPlaceService _placeService;
        readonly IVenueDtoFactory _venueDtoFactory;
        readonly IUserContext _userContext;

        public VenueDtoService(IPlaceService placeService, IVenueDtoFactory venueDtoFactory, IUserContext userContext)
        {
            _placeService = placeService;
            _venueDtoFactory = venueDtoFactory;
            _userContext = userContext;
        }

        public async Task<VenueDto> AddVenue(VenueDto venueDto, string placeId)
        {
            var venue = await _placeService.AddVenue(venueDto, placeId, _userContext.Account);
            var newVenueDto = _venueDtoFactory.Create(venue);
            return newVenueDto;
        }
    }
}