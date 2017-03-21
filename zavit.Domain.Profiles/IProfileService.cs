using System.IO;
using System.Threading.Tasks;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public interface IProfileService
    {
        Profile UpdateProfile(ProfileUpdate profileUpdate, Profile profile);
        Task<Profile> UpdateProfileImage(Stream image, Profile profile);
    }
}