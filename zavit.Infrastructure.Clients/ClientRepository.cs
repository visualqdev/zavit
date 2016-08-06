using NHibernate;
using zavit.Domain.Clients;

namespace zavit.Infrastructure.Clients
{
    public class ClientRepository : IClientRepository
    {
        readonly ISession _session;

        public ClientRepository(ISession session)
        {
            _session = session;
        }

        public Client FindClient(int clientId)
        {
            return _session.Get<Client>(clientId);
        }
    }
}