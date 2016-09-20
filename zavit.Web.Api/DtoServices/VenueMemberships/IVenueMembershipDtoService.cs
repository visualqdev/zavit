using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoServices.VenueMemberships
{
    public interface IVenueMembershipDtoService
    {
        VenueMembershipDto AddVenueMembership(VenueMembershipDto venueMembershipDto);
    }
}