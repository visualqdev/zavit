using System.Web.Http;
using zavit.Web.Api.Dtos.VenueMembers;
using zavit.Web.Api.DtoServices.VenueMembers;

namespace zavit.Web.Api.Controllers
{
    public class VenueMembersController : ApiController
    {
        readonly IVenueMemberDtoService _venueMemberDtoService;

        public VenueMembersController(IVenueMemberDtoService venueMemberDtoService)
        {
            _venueMemberDtoService = venueMemberDtoService;
        }

        [HttpGet]
        [Route("~/api/venues/{venueId}/venuemembers")]
        public VenueMembersCollectionDto Get(int venueId, int skip = 0, int take = 20)
        {
            return _venueMemberDtoService.GetMembersCollection(venueId, skip, take);
        }
    }
}