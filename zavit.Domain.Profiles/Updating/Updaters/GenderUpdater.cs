namespace zavit.Domain.Profiles.Updating.Updaters
{
    public class GenderUpdater : IProfileUpdater
    {
        public bool Update(Profile profile, ProfileUpdate profileUpdate)
        {
            if (profile.Gender == profileUpdate.Gender) return false;

            profile.Gender = profileUpdate.Gender;
            return true;
        }
    }
}