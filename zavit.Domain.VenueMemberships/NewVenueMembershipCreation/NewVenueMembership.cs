using System.Collections.Generic;

namespace zavit.Domain.VenueMemberships.NewVenueMembershipCreation
{
    public class NewVenueMembership
    {
        public int VenueId { get; set; }
        public IEnumerable<int> Activities { get; set; }
    }
}