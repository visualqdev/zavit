
namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileCreator
    {
        Profile CreateProfile(IProfileRegistration accountProfileRegistration);
    }
}