using System.Collections.Generic;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public class ProfileService : IProfileService
    {
        readonly IProfileRepository _profileRepository;
        readonly IEnumerable<IProfileUpdater> _profileUpdaters;
        readonly IProfileCreator _profileCreator;

        public ProfileService(IProfileRepository profileRepository, IEnumerable<IProfileUpdater> profileUpdaters, IProfileCreator profileCreator)
        {
            _profileRepository = profileRepository;
            _profileUpdaters = profileUpdaters;
            _profileCreator = profileCreator;
        }

        public Profile UpdateProfile(ProfileUpdate profileUpdate, Profile profile)
        {
            if (profile.AcceptUpdate(profileUpdate, _profileUpdaters))
            {
                _profileRepository.Update(profile);
            }

            return profile;
        }
        
        public Profile CreateProfile(IProfileRegistration profileRegistration)
        {
            var profile = _profileCreator.CreateProfile(profileRegistration);
            return profile;
        }
    }
}