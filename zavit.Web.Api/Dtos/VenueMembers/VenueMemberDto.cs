using System.Collections.Generic;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Dtos.VenueMembers
{
    public class VenueMemberDto
    {
        public string DisplayName { get; set; }
        public int AccountId { get; set; }
        public IEnumerable<VenueActivityDto> Activities { get; set; }
    }
}