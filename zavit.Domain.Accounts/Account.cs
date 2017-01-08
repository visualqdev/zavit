using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles;
using zavit.Domain.Shared;

namespace zavit.Domain.Accounts
{
    public class Account : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }

        public virtual AccountType AccountType { get; set; }

        public virtual Profile Profile { get; set; }

        public virtual bool VerifyPassword(string password, IAccountSecurity accountSecurity)
        {
            if (AccountType == AccountType.External)
                return false;

            return accountSecurity.ValidatePassword(password, Password);
        }
    }
}