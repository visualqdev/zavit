using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using zavit.Domain.Profiles.ProfileImages;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public class ProfileService : IProfileService
    {
        readonly IProfileRepository _profileRepository;
        readonly IEnumerable<IProfileUpdater> _profileUpdaters;
        readonly IProfileImageCreator _profileImageCreator;
        
        public ProfileService(IProfileRepository profileRepository, IEnumerable<IProfileUpdater> profileUpdaters, IProfileImageCreator profileImageCreator)
        {
            _profileRepository = profileRepository;
            _profileUpdaters = profileUpdaters;
            _profileImageCreator = profileImageCreator;
        }

        public Profile UpdateProfile(ProfileUpdate profileUpdate, Profile profile)
        {
            if (profile.AcceptUpdate(profileUpdate, _profileUpdaters))
            {
                _profileRepository.Update(profile);
            }

            return profile;
        }

        public async Task<Profile> UpdateProfileImage(Stream image, Profile profile)
        {
            var profileImage = await _profileImageCreator.Create(image);
            profile.ProfileImage = profileImage;
            _profileRepository.Update(profile);
            
            return profile;
        }
    }
}