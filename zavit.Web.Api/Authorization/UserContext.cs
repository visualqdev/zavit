using zavit.Domain.Accounts;
using zavit.Web.Authorization;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Authorization
{
    public class UserContext : IUserContext
    {
        readonly IAccountRepositoryFactory _accountRepositoryFactory;
        readonly IClaimsIdentityProvider _claimsIdentityProvider;
        public Account CachedAccount;

        public UserContext(IAccountRepositoryFactory accountRepositoryFactory, IClaimsIdentityProvider claimsIdentityProvider)
        {
            _accountRepositoryFactory = accountRepositoryFactory;
            _claimsIdentityProvider = claimsIdentityProvider;
        }

        public Account Account
        {
            get
            {
                if (CachedAccount != null) return CachedAccount;

                var username = _claimsIdentityProvider.Username;

                var accountRepository = _accountRepositoryFactory.Create();
                var account = accountRepository.Get(username);
                _accountRepositoryFactory.Release(accountRepository);
                CachedAccount = account;

                return account;
            }
        }
    }
}