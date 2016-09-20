using System.Web.Http;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships;

namespace zavit.Web.Api.Controllers
{
    public class VenueMembershipsController : ApiController
    {
        readonly IVenueMembershipDtoService _venueMembershipDtoService;

        public VenueMembershipsController(IVenueMembershipDtoService venueMembershipDtoService)
        {
            _venueMembershipDtoService = venueMembershipDtoService;
        }

        [HttpPost]
        [Authorize]
        public IHttpActionResult Post(VenueMembershipDto venueMembershipDto)
        {
            var createdVenueMembershipDto = _venueMembershipDtoService.AddVenueMembership(venueMembershipDto);
            return CreatedAtRoute(CommonRoutes.Default, new { controller = "venuememberships", id = createdVenueMembershipDto.VenueId }, createdVenueMembershipDto);
        }
    }
}