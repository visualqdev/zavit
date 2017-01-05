namespace zavit.Domain.Profiles.Registration
{
    public class ProfileCreator : IProfileCreator
    {
        readonly IProfileImageCreator _profileImageCreator;

        public ProfileCreator(IProfileImageCreator profileImageCreator)
        {
            _profileImageCreator = profileImageCreator;
        }

        public Profile CreateProfile(IProfileRegistration accountProfileRegistration)
        {
            var profileImage = _profileImageCreator.Create(accountProfileRegistration);

            return new Profile
            {
                Gender = accountProfileRegistration.Gender,
                ProfileImage = profileImage
            };
        }
    }
}