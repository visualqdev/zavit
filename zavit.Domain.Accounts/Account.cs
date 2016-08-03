using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Shared;

namespace zavit.Domain.Accounts
{
    public class Account : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string Username { get; set; }
        public virtual string Password { get; set; }
        
        public virtual bool VerifyPassword(string password, IAccountSecurity accountSecurity)
        {
            return accountSecurity.ValidatePassword(password, Password);
        }
    }
}