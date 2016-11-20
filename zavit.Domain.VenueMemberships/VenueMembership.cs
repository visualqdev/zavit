using System;
using System.Collections.Generic;
using System.Linq;
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
        public virtual DateTime CreatedOn { get; set; }

        public virtual bool UpdateActivities(IList<Activity> activities)
        {
            var existingActivityIds = new HashSet<int>(Activities.Select(a => a.Id));

            if (existingActivityIds.Count != activities.Count || activities.Any(a => !existingActivityIds.Contains(a.Id)))
            {
                Activities = activities;
                Venue.AddActivities(activities);
                return true;
            }

            return false;
        }
    }
}