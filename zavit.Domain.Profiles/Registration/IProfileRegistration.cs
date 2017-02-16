using System.IO;

namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileRegistration
    {
        Gender Gender { get; }
        Stream ProfileImage { get; }
        string Email { get; }
        string DisplayName { get; }
    }
}