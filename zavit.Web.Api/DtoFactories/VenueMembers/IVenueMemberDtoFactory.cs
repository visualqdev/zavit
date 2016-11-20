using zavit.Domain.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMembers;

namespace zavit.Web.Api.DtoFactories.VenueMembers
{
    public interface IVenueMemberDtoFactory
    {
        VenueMemberDto CreateItem(VenueMembership venueMembership);
    }
}