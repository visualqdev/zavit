using System;
using System.Collections.Generic;
using System.Web.Http;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships;

namespace zavit.Web.Api.Controllers
{
    public class VenueMembershipsController : ApiController
    {
        const string GetMembershipsRoute = "getMemberships";
        const string GetMembershipRoute = "getMembership";

        readonly IVenueMembershipDtoService _venueMembershipDtoService;
        readonly IVenueMembershipDetailsDtoService _venueMembershipDetailsDtoService;

        public VenueMembershipsController(IVenueMembershipDtoService venueMembershipDtoService, IVenueMembershipDetailsDtoService venueMembershipDetailsDtoService)
        {
            _venueMembershipDtoService = venueMembershipDtoService;
            _venueMembershipDetailsDtoService = venueMembershipDetailsDtoService;
        }

        [HttpPost]
        [Authorize]
        [Route("~/api/venuememberships")]
        public IHttpActionResult Post(VenueMembershipDto venueMembershipDto)
        {
            var createdVenueMembershipDto = _venueMembershipDtoService.AddVenueMembership(venueMembershipDto);
            return CreatedAtRoute(CommonRoutes.Default, new { controller = "venuememberships", id = createdVenueMembershipDto.Venue.Id }, createdVenueMembershipDto);
        }

        [HttpGet]
        [Authorize]
        [Route("~/api/venuememberships")]
        public IEnumerable<VenueMembershipDto> Get()
        {
            return _venueMembershipDtoService.GetVenueMemberships();
        }

        [HttpGet]
        [Authorize]
        [Route("~/api/venues/{venueId}/venuememberships", Name = GetMembershipRoute)]
        public VenueMembershipDetailsDto Get(int venueId)
        {
            return _venueMembershipDetailsDtoService.GetMembershipDetails(venueId);
        }
    }
}