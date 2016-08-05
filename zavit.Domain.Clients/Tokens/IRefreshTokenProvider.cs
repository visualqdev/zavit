namespace zavit.Domain.Clients.Tokens
{
    public interface IRefreshTokenProvider
    {
        RefreshToken Create(string tokenId, int clientiId, string userName, string protectedTicket);
    }
}