namespace zavit.Domain.Clients.Tokens
{
    public interface IRefreshTokenRepository
    {
        void Save(RefreshToken refreshToken);
        void Remove(RefreshToken refreshToken);
        RefreshToken Find(string hashedRefreshTokenId);
    }
}