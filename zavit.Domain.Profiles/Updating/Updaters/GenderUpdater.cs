using zavit.Domain.Profiles;

namespace zavit.Domain.Accounts.Updating.Updaters
{
    public class GenderUpdater : IProfileUpdater
    {
        public bool Update(Account account, ProfileUpdate profileUpdate)
        {
            if (account.Profile.Gender == profileUpdate.Gender) return false;

            account.Profile.Gender = profileUpdate.Gender;
            return true;
        }
    }
}