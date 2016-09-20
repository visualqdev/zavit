using zavit.Domain.VenueMemberships.NewVenueMembershipCreation;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoServices.VenueMemberships.NewVenueMemberships
{
    public interface INewVenueMembershipProvider
    {
        NewVenueMembership Provide(VenueMembershipDto venueMembershipDto);
    }
}