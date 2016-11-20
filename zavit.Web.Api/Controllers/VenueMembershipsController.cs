using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using zavit.Web.Api.Authorization.AccessAuthorization;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships;

namespace zavit.Web.Api.Controllers
{
    public class VenueMembershipsController : ApiController
    {
        const string GetMembershipRoute = "getMembership";

        readonly IVenueMembershipDtoService _venueMembershipDtoService;
        readonly IVenueMembershipDetailsDtoService _venueMembershipDetailsDtoService;

        public VenueMembershipsController(IVenueMembershipDtoService venueMembershipDtoService, IVenueMembershipDetailsDtoService venueMembershipDetailsDtoService)
        {
            _venueMembershipDtoService = venueMembershipDtoService;
            _venueMembershipDetailsDtoService = venueMembershipDetailsDtoService;
        }

        [HttpPost]
        [AccessAuthorize]
        [Route("~/api/venuememberships")]
        public IHttpActionResult Post(VenueMembershipDto venueMembershipDto)
        {
            var createdVenueMembershipDto = _venueMembershipDtoService.AddVenueMembership(venueMembershipDto);
            return CreatedAtRoute(CommonRoutes.Default, new { controller = "venuememberships", id = createdVenueMembershipDto.Venue.Id }, createdVenueMembershipDto);
        }

        [HttpGet]
        [AccessAuthorize]
        [Route("~/api/venuememberships")]
        public IEnumerable<VenueMembershipDto> Get()
        {
            return _venueMembershipDtoService.GetVenueMemberships();
        }

        [HttpGet]
        [AccessAuthorize]
        [Route("~/api/venues/{venueId}/venuememberships", Name = GetMembershipRoute)]
        public VenueMembershipDetailsDto Get(int venueId)
        {
            return _venueMembershipDetailsDtoService.GetMembershipDetails(venueId);
        }

        [HttpGet]
        [Route("~/api/places/{publicPlaceId}/venuememberships")]
        public async Task<VenueMembershipDetailsDto> GetAtPlace(string publicPlaceId)
        {
            return await _venueMembershipDetailsDtoService.GetMembershipDetails(publicPlaceId);
        }
    }
}