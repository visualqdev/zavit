using zavit.Domain.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public interface IMembershipVenueDtoFactory
    {
        MembershipVenueDto Create(Venue venue);
    }
}