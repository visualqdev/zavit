namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountRegistration
    {
        string DisplayName { get; }
        string Username { get; }
        string Password { get; }
    }
}