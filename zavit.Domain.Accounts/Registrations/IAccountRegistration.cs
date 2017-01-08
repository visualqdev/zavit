using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountRegistration : IProfileRegistration
    {
        string Username { get; }
        string Password { get; }
        AccountType AccountType { get; }
    }
}