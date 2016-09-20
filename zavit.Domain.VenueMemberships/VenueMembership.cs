using System.Collections.Generic;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Shared;
using zavit.Domain.Venues;

namespace zavit.Domain.VenueMemberships
{
    public class VenueMembership : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual Account Account { get; set; }
        public virtual Venue Venue { get; set; }
        public virtual IEnumerable<Activity> Activities { get; set; }
    }
}