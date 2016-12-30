using zavit.Domain.Shared;

namespace zavit.Domain.Profiles
{
    public class ProfileImage : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual byte[] ImageFile { get; set; }
    }
}