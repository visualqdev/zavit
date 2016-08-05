namespace zavit.Domain.Clients.Tokens
{
    public interface ITokenSecurity
    {
        string HashTokenId(string tokenId);
    }
}