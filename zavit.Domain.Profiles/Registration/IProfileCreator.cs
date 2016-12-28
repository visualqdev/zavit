using zavit.Domain.Accounts;

namespace zavit.Domain.Profiles.Registration
{
    public interface IProfileCreator
    {
        Profile CreateProfile(Account account, IAccountProfileRegistration accountProfileRegistration);
    }
}