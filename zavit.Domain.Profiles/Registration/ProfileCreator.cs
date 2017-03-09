using System.Threading.Tasks;
using zavit.Domain.Profiles.ProfileImages;

namespace zavit.Domain.Profiles.Registration
{
    public class ProfileCreator : IProfileCreator
    {
        readonly IProfileImageCreator _profileImageCreator;

        public ProfileCreator(IProfileImageCreator profileImageCreator)
        {
            _profileImageCreator = profileImageCreator;
        }

        public async Task<Profile> CreateProfile(IProfileRegistration accountProfileRegistration)
        {
            var profileImage = await _profileImageCreator.Create(accountProfileRegistration.ProfileImage);

            return new Profile
            {
                Gender = accountProfileRegistration.Gender,
                ProfileImage = profileImage,
                DisplayName = accountProfileRegistration.DisplayName,
                Email = accountProfileRegistration.Email
            };
        }
    }
}