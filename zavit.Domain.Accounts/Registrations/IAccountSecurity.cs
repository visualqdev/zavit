namespace zavit.Domain.Accounts.Registrations
{
    public interface IAccountSecurity
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string correctHash);
    }
}