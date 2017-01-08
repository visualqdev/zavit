using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Accounts.Profiles
{
    public interface IProfileImageCreator
    {
        ProfileImage Create(IAccountRegistration accountProfileRegistration);
    }
}