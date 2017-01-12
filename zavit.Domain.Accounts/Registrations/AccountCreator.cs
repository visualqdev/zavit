using zavit.Domain.Profiles.Registration;

namespace zavit.Domain.Accounts.Registrations
{
    public class AccountCreator : IAccountCreator
    {
        readonly IAccountSecurity _accountSecurity;
        readonly IProfileCreator _profileCreator;

        public AccountCreator(IAccountSecurity accountSecurity, IProfileCreator profileCreator)
        {
            _accountSecurity = accountSecurity;
            _profileCreator = profileCreator;
        }

        public Account Create(IAccountRegistration accountRegistration)
        {
            var account = new Account
            {
                Username = accountRegistration.Username,
                AccountType = accountRegistration.AccountType,
                Profile = _profileCreator.CreateProfile(accountRegistration)
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