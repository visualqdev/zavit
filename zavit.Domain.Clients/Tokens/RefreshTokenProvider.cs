using zavit.Domain.Shared;

namespace zavit.Domain.Clients.Tokens
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        readonly ITokenSecurity _tokenSecurity;
        readonly IClientRepository _clientRepository;
        readonly IDateTime _dateTime;

        public RefreshTokenProvider(ITokenSecurity tokenSecurity, IClientRepository clientRepository, IDateTime dateTime)
        {
            _tokenSecurity = tokenSecurity;
            _clientRepository = clientRepository;
            _dateTime = dateTime;
        }

        public RefreshToken Create(string tokenId, int clientId, string userName, string protectedTicket)
        {
            var client = _clientRepository.FindClient(clientId);

            var issueDate = _dateTime.UtcNow;
            var expiryDate = client.CalculateTokenExpiry(issueDate);

            var refreshToken = new RefreshToken
            {
                Subject = userName,
                ProtectedTicket = protectedTicket,
                Client = client,
                IssuedDateUtc = issueDate,
                ExpectedExpiryDateUtc = expiryDate,
                HashedTokenId = _tokenSecurity.HashTokenId(tokenId)
            };

            return refreshToken;
        }
    }
}