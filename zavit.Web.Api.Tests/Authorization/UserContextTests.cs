using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Web.Api.Authorization;
using zavit.Web.Core.Authorization;

namespace zavit.Web.Api.Tests.Authorization
{
    [Subject("UserContext")]
    public class UserContextTests : TestOf<UserContext>
    {
        class When_getting_a_current_user_account
        {
            Because of = () => _result = Subject.Account;

            It should_return_account_for_a_current_user = () => _result.ShouldEqual(_account);

            It should_release_the_repository = 
                () => Injected<IAccountRepositoryFactory>().AssertWasCalled(f => f.Release(_accountRepository));

            It should_cache_the_account = () => Subject.CachedAccount.ShouldEqual(_account);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                const string username = "Username";
                Injected<IClaimsIdentityProvider>().Stub(i => i.Username).Return(username);

                _accountRepository = NewInstanceOf<IAccountRepository>();
                _accountRepository.Stub(r => r.Get(username)).Return(_account);
                Injected<IAccountRepositoryFactory>().Stub(f => f.Create()).Return(_accountRepository);
            };

            static Account _result;
            static Account _account;
            static IAccountRepository _accountRepository;
        }

        class When_getting_a_current_user_account_that_has_been_previosuly_retrieved
        {
            Because of = () => _result = Subject.Account;

            It should_return_the_account_that_has_been_stored_in_the_context_already = () => _result.ShouldEqual(Subject.CachedAccount);

            Establish context = () =>
            {
                Subject.CachedAccount = NewInstanceOf<Account>();
            };

            static Account _result;
        }
    }
}