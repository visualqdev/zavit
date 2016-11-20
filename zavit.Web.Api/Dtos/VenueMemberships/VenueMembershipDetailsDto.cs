using System.Collections.Generic;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Dtos.VenueMemberships
{
    public class VenueMembershipDetailsDto
    {
        public VenueDetailsDto Venue { get; set; }
        public IEnumerable<VenueActivityDto> Activities { get; set; }
    }
}