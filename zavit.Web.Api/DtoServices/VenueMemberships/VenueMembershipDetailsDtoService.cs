using System.Threading.Tasks;
using zavit.Domain.VenueMemberships;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.VenueMemberships
{
    public class VenueMembershipDetailsDtoService : IVenueMembershipDetailsDtoService
    {
        readonly IUserContext _userContext;
        readonly IVenueMembershipService _venueMembershipService;
        readonly IVenueMembershipDetailsDtoFactory _venueMembershipDetailsDtoFactory;
        readonly IVenueService _venueService;
        readonly IVenueRepository _venueRepository;

        public VenueMembershipDetailsDtoService(IUserContext userContext, IVenueMembershipService venueMembershipService, IVenueMembershipDetailsDtoFactory venueMembershipDetailsDtoFactory, IVenueService venueService, IVenueRepository venueRepository)
        {
            _userContext = userContext;
            _venueMembershipService = venueMembershipService;
            _venueMembershipDetailsDtoFactory = venueMembershipDetailsDtoFactory;
            _venueService = venueService;
            _venueRepository = venueRepository;
        }

        public VenueMembershipDetailsDto GetMembershipDetails(int venueId)
        {
            var account = _userContext.Account;
            var venueMembership = _venueMembershipService.GetVenueMembership(account, venueId);

            if (venueMembership != null)
                return _venueMembershipDetailsDtoFactory.CreateItem(venueMembership);

            var venue = _venueRepository.GetVenue(venueId);
            return _venueMembershipDetailsDtoFactory.CreateItem(venue);
        }

        public async Task<VenueMembershipDetailsDto> GetMembershipDetails(string publicPlaceId)
        {
            var account = _userContext.Account;
            var venueMembership = _venueMembershipService.GetVenueMembership(account, publicPlaceId);

            if (venueMembership != null)
                return _venueMembershipDetailsDtoFactory.CreateItem(venueMembership);

            var venue = await _venueService.GetDefaultVenue(publicPlaceId);
            return _venueMembershipDetailsDtoFactory.CreateItem(venue);
        }
    }
}