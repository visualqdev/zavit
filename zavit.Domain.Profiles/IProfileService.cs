using System.IO;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public interface IProfileService
    {
        Profile UpdateProfile(ProfileUpdate profileUpdate, Profile profile);
        Profile UpdateProfileImage(Stream image, Profile profile);
    }
}