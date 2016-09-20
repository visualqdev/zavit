using System.Collections.Generic;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Dtos.VenueMemberships
{
    public class VenueMembershipDto
    {
        public int VenueId { get; set; }
        public IEnumerable<VenueActivityDto> Activities { get; set; }
    }
}