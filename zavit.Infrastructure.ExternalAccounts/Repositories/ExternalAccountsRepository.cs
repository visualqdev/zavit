using NHibernate;
using zavit.Domain.ExternalAccounts;

namespace zavit.Infrastructure.ExternalAccounts.Repositories
{
    public class ExternalAccountsRepository : IExternalAccountsRepository
    {
        readonly ISession _session;

        public ExternalAccountsRepository(ISession session)
        {
            _session = session;
        }

        public bool CheckIfExists(string loginProvider, string providerKey)
        {
            return _session.QueryOver<ExternalAccount>()
                    .Where(a => a.LoginProvider == loginProvider && a.ProviderKey == providerKey)
                    .RowCount() > 0;
        }

        public void Save(ExternalAccount externalAccount)
        {
            _session.Save(externalAccount);
            _session.Flush();

        }

        public ExternalAccount Find(string loginProvider, string providerKey)
        {
            return _session.QueryOver<ExternalAccount>()
                .Where(a => a.LoginProvider == loginProvider && a.ProviderKey == providerKey)
                .SingleOrDefault();
        }
    }
}