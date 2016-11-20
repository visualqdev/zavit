using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues.NewVenues;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.Venues
{
    public class VenueDtoService : IVenueDtoService
    {
        readonly IUserContext _userContext;
        readonly IVenueDetailsDtoFactory _venueDetailsDtoFactory;
        readonly INewVenueProvider _newVenueProvider;
        readonly IVenueRepository _venueRepository;
        readonly IVenueService _venueService;
        readonly IVenueDtoFactory _venueDtoFactory;

        public VenueDtoService(IUserContext userContext, IVenueDetailsDtoFactory venueDetailsDtoFactory, INewVenueProvider newVenueProvider, IVenueRepository venueRepository, IVenueService venueService, IVenueDtoFactory venueDtoFactory)
        {
            _userContext = userContext;
            _venueDetailsDtoFactory = venueDetailsDtoFactory;
            _newVenueProvider = newVenueProvider;
            _venueRepository = venueRepository;
            _venueService = venueService;
            _venueDtoFactory = venueDtoFactory;
        }

        public async Task<VenueDetailsDto> AddVenue(VenueDetailsDto venueDetailsDto)
        {
            var newVenueRequest = _newVenueProvider.ProvideNewVenueRequest(venueDetailsDto);
            var venue = await _venueService.CreateVenue(newVenueRequest, _userContext.Account);
            var newVenueDetailsDto = _venueDetailsDtoFactory.Create(venue);
            return newVenueDetailsDto;
        }

        public async Task<VenueDetailsDto> GetDefaultVenue(string placeId)
        {
            var venue = await _venueService.GetDefaultVenue(placeId);
            var venueDetailsDto = _venueDetailsDtoFactory.Create(venue);
            return venueDetailsDto;
        }

        public VenueDetailsDto GetVenue(int venueId)
        {
            var venue = _venueRepository.GetVenue(venueId);
            var venueDetailsDto = _venueDetailsDtoFactory.Create(venue);
            return venueDetailsDto;
        }

        public async Task<IEnumerable<VenueDto>> SuggestVenues(VenueSearchCriteriaDto venueSearchCriteriaDto)
        {
            var venues = await _venueService.SuggestVenues(venueSearchCriteriaDto);
            var venueDtos = venues.Select(v => _venueDtoFactory.Create(v));
            return venueDtos;
        }
    }
}