namespace zavit.Domain.Profiles.Updating.Updaters
{
    public class EmailUpdater : IProfileUpdater
    {
        public bool Update(Profile profile, ProfileUpdate profileUpdate)
        {
            if (string.IsNullOrWhiteSpace(profileUpdate.Email) ||
                profileUpdate.Email == profile.Email)
                return false;

            profile.Email = profileUpdate.Email;
            return true;
        }
    }
}