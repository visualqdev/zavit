using zavit.Domain.Clients;
using zavit.Domain.Clients.Tokens;

namespace zavit.Web.Core.Authorization
{
    public interface IRefreshTokenRepositoryFactory
    {
        IRefreshTokenRepository Create();
        void Release(IRefreshTokenRepository refreshTokenRepository);
    }
}