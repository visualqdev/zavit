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

        public void Save(RefreshToken refreshToken)
        {
            _session.Save(refreshToken);
            _session.Flush();
        }

        public void Remove(RefreshToken refreshToken)
        {
            _session.Delete(refreshToken);
            _session.Flush();
        }

        public RefreshToken Find(string hashedRefreshTokenId)
        {
            return
                _session.QueryOver<RefreshToken>()
                .Where(t => t.HashedTokenId == hashedRefreshTokenId)
                .SingleOrDefault();
        }
    }
}