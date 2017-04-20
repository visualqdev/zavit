using System.Threading.Tasks;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Shared;

namespace zavit.Domain.Accounts.Registrations
{
    public class AccountCreator : IAccountCreator
    {
        readonly IAccountSecurity _accountSecurity;
        readonly IProfileCreator _profileCreator;
        readonly IDateTime _dateTime;
        readonly IGuid _guid;

        public AccountCreator(IAccountSecurity accountSecurity, IProfileCreator profileCreator, IDateTime dateTime, IGuid guid)
        {
            _accountSecurity = accountSecurity;
            _profileCreator = profileCreator;
            _dateTime = dateTime;
            _guid = guid;
        }

        public async Task<Account> Create(IAccountRegistration accountRegistration)
        {
            var currentDate = _dateTime.UtcNow;

            var account = new Account
            {
                Username = accountRegistration.Username,
                AccountType = accountRegistration.AccountType,
                Profile = await _profileCreator.CreateProfile(accountRegistration),
                DateCreated = currentDate,
                VerificationCode = _guid.NewGuidString()
            };

            if (accountRegistration.AccountType == AccountType.External)
            {
                account.DateVerified = currentDate;
                return account;
            }
            
            var password = _accountSecurity.HashPassword(accountRegistration.Password);
            account.Password = password;
            return account;
        }
    }
}