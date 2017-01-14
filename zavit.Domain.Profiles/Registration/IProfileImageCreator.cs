using System.IO;

namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileImageCreator
    {
        ProfileImage Create(Stream image);
    }
}