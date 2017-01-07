namespace zavit.Domain.Profiles.Updating.Updaters
{
    public class AboutUpdater : IProfileUpdater
    {
        public bool Update(Profile profile, ProfileUpdate profileUpdate)
        {
            if (profile.About == profileUpdate.About) return false;

            profile.About = profileUpdate.About;
            return true;
        }
    }
}