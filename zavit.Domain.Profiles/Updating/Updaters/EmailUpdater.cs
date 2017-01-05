using zavit.Domain.Profiles;

namespace zavit.Domain.Accounts.Updating.Updaters
{
    public class EmailUpdater : IProfileUpdater
    {
        public bool Update(Account account, ProfileUpdate profileUpdate)
        {
            if (string.IsNullOrWhiteSpace(profileUpdate.Email) ||
                profileUpdate.Email == account.Email)
                return false;

            account.Email = profileUpdate.Email;
            return true;
        }
    }
}