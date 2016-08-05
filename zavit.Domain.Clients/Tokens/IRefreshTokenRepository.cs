namespace zavit.Domain.Clients.Tokens
{
    public interface IRefreshTokenRepository
    {
        void Save(RefreshToken token);
    }
}