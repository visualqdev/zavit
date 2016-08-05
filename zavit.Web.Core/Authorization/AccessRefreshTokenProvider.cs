using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Infrastructure;

namespace zavit.Web.Core.Authorization
{
    public class AccessRefreshTokenProvider : IAuthenticationTokenProvider
    {
        readonly IRefreshTokenRepositoryFactory _refreshTokenRepositoryFactory;
        readonly IRefreshTokenProviderFactory _refreshTokenProviderFactory;

        public AccessRefreshTokenProvider(IRefreshTokenRepositoryFactory refreshTokenRepositoryFactory, IRefreshTokenProviderFactory refreshTokenProviderFactory)
        {
            _refreshTokenRepositoryFactory = refreshTokenRepositoryFactory;
            _refreshTokenProviderFactory = refreshTokenProviderFactory;
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return Task.FromResult(0);
            }

            var refreshTokenId = Guid.NewGuid().ToString("n");

            var refreshTokenProvider = _refreshTokenProviderFactory.Create();
            var token = refreshTokenProvider.Create(refreshTokenId, int.Parse(clientid), context.Ticket.Identity.Name, context.SerializeTicket());
            _refreshTokenProviderFactory.Release(refreshTokenProvider);

            context.Ticket.Properties.IssuedUtc = token.IssuedDateUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpectedExpiryDateUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var refreshTokenRepository = _refreshTokenRepositoryFactory.Create();
            refreshTokenRepository.Save(token);
            _refreshTokenRepositoryFactory.Release(refreshTokenRepository);

            context.SetToken(refreshTokenId);
            return Task.FromResult(0);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new System.NotImplementedException();
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}