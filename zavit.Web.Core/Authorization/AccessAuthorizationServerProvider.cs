using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Clients;

namespace zavit.Web.Core.Authorization
{
    public class AccessAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        readonly IAccountRepositoryFactory _accountRepositoryFactory;
        readonly IClientRepositoryFactory _clientRepositoryFactory;
        readonly IAccountSecurity _accountSecurity;
        
        public AccessAuthorizationServerProvider(IAccountRepositoryFactory accountRepositoryFactory, IClientRepositoryFactory clientRepositoryFactory, IAccountSecurity accountSecurity)
        {
            _accountRepositoryFactory = accountRepositoryFactory;
            _clientRepositoryFactory = clientRepositoryFactory;
            _accountSecurity = accountSecurity;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            var clientId = string.Empty;
            var clientSecret = string.Empty;

            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //Remove the comments from the below line context.SetError, and invalidate context 
                //if you want to force sending clientId/secrects once obtain access tokens. 
                context.Validated();
                //context.SetError("invalid_clientId", "ClientId should be sent.");
                return Task.FromResult(0);
            }

            var clientRepository = _clientRepositoryFactory.Create();
            var client = clientRepository.FindClient(int.Parse(context.ClientId));
            _clientRepositoryFactory.Release(clientRepository);

            if (client == null)
            {
                context.SetError("invalid_clientId", $"Client '{context.ClientId}' is not registered in the system.");
                return Task.FromResult(0);
            }

            if (client.CanProvideSecret)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret should be sent.");
                    return Task.FromResult(0);
                }
                if (!client.ValidateSecret(clientSecret))
                {
                    context.SetError("invalid_clientId", "Client secret is invalid.");
                    return Task.FromResult(0);
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult(0);
            }

            context.OwinContext.Set("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var accountRepository = _accountRepositoryFactory.Create();
            var account = accountRepository.Get(context.UserName);
            _accountRepositoryFactory.Release(accountRepository);

            if (account == null || !account.VerifyPassword(context.Password, _accountSecurity))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return Task.FromResult(0);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()));


            var authenticationProperties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", context.ClientId ?? string.Empty
                    },
                    {
                        "userName", context.UserName
                    }
                });
            context.Validated(new AuthenticationTicket(identity, authenticationProperties));
            return Task.FromResult(0);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (var property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult(0);
        }
    }
}