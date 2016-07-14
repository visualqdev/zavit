using zavit.Domain.Shared;

namespace zavit.Domain.Venues
{
    public class Venue : IEntity<int>
    {
        public virtual string Name { get; set; }
        public virtual int Id { get; set; }
    }
}