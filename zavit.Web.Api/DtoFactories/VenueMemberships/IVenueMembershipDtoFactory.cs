using zavit.Domain.VenueMemberships;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public interface IVenueMembershipDtoFactory
    {
        VenueMembershipDto CreateItem(VenueMembership venueMembership);
    }
}