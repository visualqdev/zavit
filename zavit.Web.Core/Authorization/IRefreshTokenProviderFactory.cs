using zavit.Domain.Clients;
using zavit.Domain.Clients.Tokens;

namespace zavit.Web.Core.Authorization
{
    public interface IRefreshTokenProviderFactory
    {
        IRefreshTokenProvider Create();
        void Release(IRefreshTokenProvider refreshTokenProvider);
    }
}