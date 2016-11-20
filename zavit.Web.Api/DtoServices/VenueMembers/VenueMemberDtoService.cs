using zavit.Domain.VenueMemberships;
using zavit.Web.Api.DtoFactories.VenueMembers;
using zavit.Web.Api.Dtos.VenueMembers;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.DtoServices.VenueMembers
{
    public class VenueMemberDtoService : IVenueMemberDtoService
    {
        readonly IVenueMembershipService _venueMembershipService;
        readonly IVenueMemberCollectionDtoFactory _venueMemberCollectionDtoFactory;
        readonly IUserContext _userContext;

        public VenueMemberDtoService(IVenueMembershipService venueMembershipService, IVenueMemberCollectionDtoFactory venueMemberCollectionDtoFactory, IUserContext userContext)
        {
            _venueMembershipService = venueMembershipService;
            _venueMemberCollectionDtoFactory = venueMemberCollectionDtoFactory;
            _userContext = userContext;
        }

        public VenueMembersCollectionDto GetMembersCollection(int venueId, int skip, int take)
        {
            var venueMembersCollection = _venueMembershipService.GetAllVenueMemberships(venueId, skip, take, _userContext.Account);
            var venueMembersCollectionDto = _venueMemberCollectionDtoFactory.CreateItem(venueMembersCollection);
            return venueMembersCollectionDto;
        }
    }
}