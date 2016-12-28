using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public interface IProfileService
    {
        Profile Update(ProfileUpdate profileUpdate);
        AccountRegistrationResult Register(IAccountProfileRegistration accountProfileRegistration);
    }
}