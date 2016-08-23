namespace zavit.Domain.Clients.Tokens
{
    public interface IRefreshTokenProvider
    {
        RefreshToken Create(string refreshTokenId, int clientiId, string userName, string protectedTicket);
        RefreshToken FindExisting(string refreshTokenId);
    }
}