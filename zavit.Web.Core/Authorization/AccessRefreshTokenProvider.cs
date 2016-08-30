using System;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Serializer;
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
            
            token.ProtectedTicket = SerializeTicket(context);

            var refreshTokenRepository = _refreshTokenRepositoryFactory.Create();
            refreshTokenRepository.Save(token);
            _refreshTokenRepositoryFactory.Release(refreshTokenRepository);

            context.SetToken(refreshTokenId);
            return Task.FromResult(0);
        }

        public Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var refreshTokenProvider = _refreshTokenProviderFactory.Create();
            var refreshToken = refreshTokenProvider.FindExisting(context.Token);
            _refreshTokenProviderFactory.Release(refreshTokenProvider);

            if (refreshToken == null) return Task.FromResult(0);

            //Get protectedTicket from refreshToken class
            var ticket = DeserializeTicket(refreshToken.ProtectedTicket);
            context.SetTicket(ticket);

            //remove the refresh token to only allow one refresh token per user per client
            var refreshTokenRepository = _refreshTokenRepositoryFactory.Create();
            refreshTokenRepository.Remove(refreshToken);
            _refreshTokenRepositoryFactory.Release(refreshTokenRepository);

            return Task.FromResult(0);
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            CreateAsync(context);
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            ReceiveAsync(context);
        }

        string SerializeTicket(AuthenticationTokenCreateContext context)
        {
            var serializer = new TicketSerializer();
            return System.Text.Encoding.Default.GetString(serializer.Serialize(context.Ticket));
        }

        AuthenticationTicket DeserializeTicket(string protectedTicket)
        {
            var serializer = new TicketSerializer();
            return serializer.Deserialize(System.Text.Encoding.Default.GetBytes(protectedTicket));
        }
    }
}