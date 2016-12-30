using zavit.Domain.Accounts;

namespace zavit.Domain.Profiles.Registration
{
    public class ProfileCreator : IProfileCreator
    {
        readonly IProfileImageCreator _profileImageCreator;

        public ProfileCreator(IProfileImageCreator profileImageCreator)
        {
            _profileImageCreator = profileImageCreator;
        }

        public Profile CreateProfile(Account account, IAccountProfileRegistration accountProfileRegistration)
        {
            var profileImage = _profileImageCreator.Create(accountProfileRegistration);

            return new Profile
            {
                Account = account,
                Gender = accountProfileRegistration.Gender,
                ProfileImage = profileImage
            };
        }
    }
}