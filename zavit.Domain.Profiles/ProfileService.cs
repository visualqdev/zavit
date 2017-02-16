using System.Collections.Generic;
using System.IO;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public class ProfileService : IProfileService
    {
        readonly IProfileRepository _profileRepository;
        readonly IEnumerable<IProfileUpdater> _profileUpdaters;
        readonly IProfileImageCreator _profileImageCreator;
        readonly IProfileImageRepository _profileImageRepository;

        public ProfileService(IProfileRepository profileRepository, IEnumerable<IProfileUpdater> profileUpdaters, IProfileImageCreator profileImageCreator, IProfileImageRepository profileImageRepository)
        {
            _profileRepository = profileRepository;
            _profileUpdaters = profileUpdaters;
            _profileImageCreator = profileImageCreator;
            _profileImageRepository = profileImageRepository;
        }

        public Profile UpdateProfile(ProfileUpdate profileUpdate, Profile profile)
        {
            if (profile.AcceptUpdate(profileUpdate, _profileUpdaters))
            {
                _profileRepository.Update(profile);
            }

            return profile;
        }

        public Profile UpdateProfileImage(Stream image, Profile profile)
        {
            var oldProfileImageId = profile.ProfileImage;

            var profileImage = _profileImageCreator.Create(image);
            profile.ProfileImage = profileImage;
            _profileRepository.Update(profile);

            if (oldProfileImageId != null)
            {
                _profileImageRepository.RemoveImage(oldProfileImageId);
            }

            return profile;
        }
    }
}