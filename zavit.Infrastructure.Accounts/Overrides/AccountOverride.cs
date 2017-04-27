using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using zavit.Domain.Accounts;

namespace zavit.Infrastructure.Accounts.Overrides
{
    public class AccountOverride : IAutoMappingOverride<Account>
    {
        public void Override(AutoMapping<Account> mapping)
        {
            mapping.IgnoreProperty(a => a.NeedsVerification);
        }
    }
}