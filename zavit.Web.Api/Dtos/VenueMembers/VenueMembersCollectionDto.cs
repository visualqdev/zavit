using System.Collections.Generic;

namespace zavit.Web.Api.Dtos.VenueMembers
{
    public class VenueMembersCollectionDto
    {
        public bool HasMoreResults { get; set; }
        public int Skip { get; set; }
        public int Take { get; set; }
        public IEnumerable<VenueMemberDto> Members { get; set; }
    }
}