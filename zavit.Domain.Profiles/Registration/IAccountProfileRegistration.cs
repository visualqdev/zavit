using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Profiles.Registration
{
    public interface IAccountProfileRegistration : IAccountRegistration
    {
        Gender Gender { get; }
        byte[] ProfileImage { get; }
    }
}