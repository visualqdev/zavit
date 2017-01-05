using zavit.Domain.Accounts;

namespace zavit.Domain.Profiles.Updating.Updaters
{
    public class AboutUpdater : IProfileUpdater
    {
        public bool Update(Account account, ProfileUpdate profileUpdate)
        {
            if (account.Profile.About == profileUpdate.About) return false;

            account.Profile.About = profileUpdate.About;
            return true;
        }
    }
}