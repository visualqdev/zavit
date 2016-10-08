using System.Collections.Generic;
using System.Linq;
using zavit.Domain.Accounts;
using zavit.Domain.Activities;
using zavit.Domain.Shared;

namespace zavit.Domain.Venues
{
    public class Venue : IEntity<int>
    {
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        public virtual Account OwnerAccount { get; set; }
        public virtual IList<Activity> Activities { get; set; }
        public virtual string Address { get; set; }
        public virtual double Longitude { get; set; }
        public virtual double Latitude { get; set; }

        public virtual void AddActivities(IEnumerable<Activity> activities)
        {
            var existingActivityIds = new HashSet<int>(Activities.Select(a => a.Id));
            foreach (var activity in activities)
            {
                if(!existingActivityIds.Contains(activity.Id))
                    Activities.Add(activity);
            }
        }
    }
}