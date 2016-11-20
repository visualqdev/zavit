using zavit.Domain.VenueMemberships;
using zavit.Domain.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public interface IVenueMembershipDetailsDtoFactory
    {
        VenueMembershipDetailsDto CreateItem(VenueMembership venueMembership);

        VenueMembershipDetailsDto CreateItem(Venue venue);
    }
}