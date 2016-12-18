using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public interface IProfileService
    {
        Profile Update(ProfileUpdate profileUpdate);
    }
}