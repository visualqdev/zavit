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
        readonly IVenueDetailsDtoFactory _venueDetailsDtoFactory;

        public VenueDtoService(IPlaceService placeService, IVenueDtoFactory venueDtoFactory, IUserContext userContext, IVenueDetailsDtoFactory venueDetailsDtoFactory)
        {
            _placeService = placeService;
            _venueDtoFactory = venueDtoFactory;
            _userContext = userContext;
            _venueDetailsDtoFactory = venueDetailsDtoFactory;
        }

        public async Task<VenueDto> AddVenue(VenueDto venueDto, string placeId)
        {
            var venue = await _placeService.AddVenue(venueDto, placeId, _userContext.Account);
            var newVenueDto = _venueDtoFactory.Create(venue);
            return newVenueDto;
        }

        public async Task<VenueDetailsDto> GetDefaultVenue(string placeId)
        {
            var venue = await _placeService.GetDefaultVenue(placeId);
            var venueDetailsDto = _venueDetailsDtoFactory.Create(venue);
            return venueDetailsDto;
        }
    }
}