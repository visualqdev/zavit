using System.Collections.Generic;
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
        public virtual IEnumerable<Activity> Activities { get; set; }
    }
}