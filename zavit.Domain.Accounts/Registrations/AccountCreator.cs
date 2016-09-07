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
                DisplayName = accountRegistration.DisplayName,
                Email = accountRegistration.Email,
                AccountType = accountRegistration.AccountType
            };

            if (accountRegistration.AccountType == AccountType.Internal)
            {
                var password = _accountSecurity.HashPassword(accountRegistration.Password);
                account.Password = password;
            }

            return account;
        }
    }
}