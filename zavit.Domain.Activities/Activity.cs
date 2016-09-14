using zavit.Domain.Shared;

namespace zavit.Domain.Activities
{
    public class Activity : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}