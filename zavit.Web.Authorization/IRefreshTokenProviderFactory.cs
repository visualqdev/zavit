using zavit.Domain.Clients.Tokens;

namespace zavit.Web.Authorization
{
    public interface IRefreshTokenProviderFactory
    {
        IRefreshTokenProvider Create();
        void Release(IRefreshTokenProvider refreshTokenProvider);
    }
}