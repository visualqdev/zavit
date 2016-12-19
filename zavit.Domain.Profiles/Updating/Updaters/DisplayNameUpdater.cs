namespace zavit.Domain.Profiles.Updating.Updaters
{
    public class DisplayNameUpdater : IProfileUpdater
    {
        public bool Update(Profile profile, ProfileUpdate profileUpdate)
        {
            if (string.IsNullOrWhiteSpace(profileUpdate.DisplayName) ||
                profileUpdate.DisplayName == profile.Account.DisplayName)
                return false;

            profile.Account.DisplayName = profileUpdate.DisplayName;
            return true;
        }
    }
}