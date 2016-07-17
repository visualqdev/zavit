namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountRegistration
    {
        string Username { get; }
        string Password { get; }
    }
}