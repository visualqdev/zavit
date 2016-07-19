using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using zavit.Domain.Accounts.Registrations;
using zavit.Web.Core.Context;

namespace zavit.Web.Core.Authorization
{
    public class AccessAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        readonly IAccountRepositoryFactory _accountRepositoryFactory;
        readonly IAccountSecurity _accountSecurity;
        readonly IUserContextIocFactory _userContextIocFactory;

        public AccessAuthorizationServerProvider(IAccountRepositoryFactory accountRepositoryFactory, IAccountSecurity accountSecurity, IUserContextIocFactory userContextIocFactory)
        {
            _accountRepositoryFactory = accountRepositoryFactory;
            _accountSecurity = accountSecurity;
            _userContextIocFactory = userContextIocFactory;
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
            var userContext = _userContextIocFactory.Create();
            var account = accountRepository.Get(context.UserName);
            _accountRepositoryFactory.Release(accountRepository);
            _userContextIocFactory.Release(userContext);

            if (account == null || !account.VerifyPassword(context.Password, _accountSecurity))
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return Task.FromResult(0);
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));
            identity.AddClaim(new Claim("accountid", account.Id.ToString()));

            context.Validated(identity);
            return Task.FromResult(0);
        }
    }
}