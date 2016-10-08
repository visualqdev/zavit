using Microsoft.AspNet.Identity;
using zavit.Domain.VenueMemberships;
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

        public VenueMembershipDetailsDtoService(IUserContext userContext, IVenueMembershipService venueMembershipService, IVenueMembershipDetailsDtoFactory venueMembershipDetailsDtoFactory)
        {
            _userContext = userContext;
            _venueMembershipService = venueMembershipService;
            _venueMembershipDetailsDtoFactory = venueMembershipDetailsDtoFactory;
        }

        public VenueMembershipDetailsDto GetMembershipDetails(int venueId)
        {
            var account = _userContext.Account;
            var venueMembership = _venueMembershipService.GetVenueMembership(account, venueId);

            var venueMembershipDetailsDto = _venueMembershipDetailsDtoFactory.CreateItem(venueMembership);
            return venueMembershipDetailsDto;
        }
    }
}