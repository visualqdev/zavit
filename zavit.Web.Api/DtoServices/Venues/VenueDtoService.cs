using System.Threading.Tasks;
using zavit.Domain.Places;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues.NewVenues;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Venues
{
    public class VenueDtoService : IVenueDtoService
    {
        readonly IPlaceService _placeService;
        readonly IUserContext _userContext;
        readonly IVenueDetailsDtoFactory _venueDetailsDtoFactory;
        readonly INewVenueProvider _newVenueProvider;

        public VenueDtoService(IPlaceService placeService, IUserContext userContext, IVenueDetailsDtoFactory venueDetailsDtoFactory, INewVenueProvider newVenueProvider)
        {
            _placeService = placeService;
            _userContext = userContext;
            _venueDetailsDtoFactory = venueDetailsDtoFactory;
            _newVenueProvider = newVenueProvider;
        }

        public async Task<VenueDetailsDto> AddVenue(VenueDetailsDto venueDetailsDto, string placeId)
        {
            var newVenueRequest = _newVenueProvider.ProvideNewVenueRequest(venueDetailsDto);
            var venue = await _placeService.AddVenue(newVenueRequest, placeId, _userContext.Account);
            var newVenueDetailsDto = _venueDetailsDtoFactory.Create(venue);
            return newVenueDetailsDto;
        }

        public async Task<VenueDetailsDto> GetDefaultVenue(string placeId)
        {
            var venue = await _placeService.GetDefaultVenue(placeId);
            var venueDetailsDto = _venueDetailsDtoFactory.Create(venue);
            return venueDetailsDto;
        }
    }
}