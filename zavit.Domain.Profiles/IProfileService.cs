using zavit.Domain.Profiles.Registration;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public interface IProfileService
    {
        Profile UpdateProfile(ProfileUpdate profileUpdate, Profile profile);
        Profile CreateProfile(IProfileRegistration profileRegistration);
    }
}