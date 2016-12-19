using zavit.Domain.Accounts;

namespace zavit.Domain.Profiles.Registration
{
    public class ProfileCreator : IProfileCreator
    {
        public Profile CreateProfile(Account account)
        {
            return new Profile
            {
                Account = account,
                Gender = Gender.NotSpecified
            };
        }
    }
}