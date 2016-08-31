using zavit.Domain.Accounts;
using zavit.Domain.Shared;

namespace zavit.Domain.ExternalAccounts
{
    public class ExternalAccount : IEntity<int>
    {
        public virtual int Id { get; set; }
        public virtual string LoginProvider { get; set; }
        public virtual string ProviderKey { get; set; }
        public virtual Account Account { get; set; }
    }
}