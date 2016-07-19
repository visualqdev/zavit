using zavit.Domain.Accounts;
using zavit.Domain.Shared;

namespace zavit.Domain.Venues
{
    public class Venue : IEntity<int>
    {
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
        public virtual Account Account { get; set; }
    }
}