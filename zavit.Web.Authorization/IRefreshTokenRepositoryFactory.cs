using zavit.Domain.Clients.Tokens;

namespace zavit.Web.Authorization
{
    public interface IRefreshTokenRepositoryFactory
    {
        IRefreshTokenRepository Create();
        void Release(IRefreshTokenRepository refreshTokenRepository);
    }
}