using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Accounts.Profiles
{
    public class ProfileImageCreator : IProfileImageCreator
    {
        public ProfileImage Create(IAccountRegistration accountProfileRegistration)
        {
            if (accountProfileRegistration.ProfileImage == null) return null;

            return new ProfileImage
            {
                ImageFile = accountProfileRegistration.ProfileImage
            };
        }
    }
}