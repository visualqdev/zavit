namespace zavit.Domain.Profiles.Updating.Updaters
{
    public class EmailUpdater : IProfileUpdater
    {
        public bool Update(Profile profile, ProfileUpdate profileUpdate)
        {
            if (string.IsNullOrWhiteSpace(profileUpdate.Email) ||
                profileUpdate.Email == profile.Account.Email)
                return false;

            profile.Account.Email = profileUpdate.Email;
            return true;
        }
    }
}