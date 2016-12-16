using System.Collections.Generic;
using zavit.Domain.Profiles.Updating;

namespace zavit.Domain.Profiles
{
    public class ProfileService : IProfileService
    {
        readonly IProfileRepository _profileRepository;
        readonly IEnumerable<IProfileUpdater> _profileUpdaters;

        public ProfileService(IProfileRepository profileRepository, IEnumerable<IProfileUpdater> profileUpdaters)
        {
            _profileRepository = profileRepository;
            _profileUpdaters = profileUpdaters;
        }

        public Profile Update(ProfileUpdate profileUpdate)
        {
            var profile = _profileRepository.GetForAccount(profileUpdate.Account.Id);
            if (profile.AcceptUpdate(profileUpdate, _profileUpdaters))
            {
                _profileRepository.Update(profile);
            }

            return profile;
        }
    }
}