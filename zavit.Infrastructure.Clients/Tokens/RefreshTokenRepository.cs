using NHibernate;
using zavit.Domain.Clients.Tokens;

namespace zavit.Infrastructure.Clients.Tokens
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        readonly ISession _session;

        public RefreshTokenRepository(ISession session)
        {
            _session = session;
        }

        public void Save(RefreshToken token)
        {
            _session.Save(token);
            _session.Flush();
        }
    }
}