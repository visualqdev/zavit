﻿using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using zavit.Domain.Accounts;

namespace zavit.Web.Core.Authorization
{
    public class AccessAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        readonly IAccountRepositoryFactory _accountRepositoryFactory;
        
        public AccessAuthorizationServerProvider(IAccountRepositoryFactory accountRepositoryFactory)
        {
            _accountRepositoryFactory = accountRepositoryFactory;
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult(0);
        }

        public override Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            var accountRepository = _accountRepositoryFactory.Create();
            var account = accountRepository.Get(context.UserName);
            _accountRepositoryFactory.Release(accountRepository);

            if (account == null || account.VerifyPassword(context.Password))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return Task.FromResult(0);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);
            return Task.FromResult(0);
        }
    }
}