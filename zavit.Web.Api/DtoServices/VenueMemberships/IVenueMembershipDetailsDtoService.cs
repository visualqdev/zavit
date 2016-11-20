using System;
using System.Threading.Tasks;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoServices.VenueMemberships
{
    public interface IVenueMembershipDetailsDtoService
    {
        VenueMembershipDetailsDto GetMembershipDetails(int venueId);
        Task<VenueMembershipDetailsDto> GetMembershipDetails(string publicPlaceId);
    }
}