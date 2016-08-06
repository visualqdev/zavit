using zavit.Domain.Shared;

namespace zavit.Domain.Clients.Tokens
{
    public class RefreshTokenProvider : IRefreshTokenProvider
    {
        readonly ITokenSecurity _tokenSecurity;
        readonly IClientRepository _clientRepository;
        readonly IDateTime _dateTime;
        readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenProvider(ITokenSecurity tokenSecurity, IClientRepository clientRepository, IDateTime dateTime, IRefreshTokenRepository refreshTokenRepository)
        {
            _tokenSecurity = tokenSecurity;
            _clientRepository = clientRepository;
            _dateTime = dateTime;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public RefreshToken Create(string refreshTokenId, int clientId, string userName, string protectedTicket)
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
                HashedTokenId = _tokenSecurity.HashTokenId(refreshTokenId)
            };

            return refreshToken;
        }

        public RefreshToken FindExisting(string refreshTokenId)
        {
            var hashedRefreshTokenId = _tokenSecurity.HashTokenId(refreshTokenId);
            return _refreshTokenRepository.Find(hashedRefreshTokenId);
        }
    }
}