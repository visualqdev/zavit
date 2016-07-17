namespace zavit.Domain.Accounts.Registrations
{
    public class AccountCreator : IAccountCreator
    {
        readonly IAccountSecurity _accountSecurity;

        public AccountCreator(IAccountSecurity accountSecurity)
        {
            _accountSecurity = accountSecurity;
        }

        public Account Create(IAccountRegistration accountRegistration)
        {
            var account = new Account
            {
                Username = accountRegistration.Username,
            };

            var password = _accountSecurity.HashPassword(accountRegistration.Password);
            account.Password = password;
            return account;
        }
    }
}