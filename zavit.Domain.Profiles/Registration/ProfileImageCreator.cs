namespace zavit.Domain.Profiles.Registration
{
    public class ProfileImageCreator : IProfileImageCreator
    {
        public ProfileImage Create(IAccountProfileRegistration accountProfileRegistration)
        {
            if (accountProfileRegistration.ProfileImage == null) return null;

            return new ProfileImage
            {
                ImageFile = accountProfileRegistration.ProfileImage
            };
        }
    }
}