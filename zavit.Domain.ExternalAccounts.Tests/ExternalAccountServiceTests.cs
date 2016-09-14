using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Domain.ExternalAccounts.Tests 
{
    [Subject("ExternalAccountService")]
    public class ExternalAccountServiceTests : TestOf<ExternalAccountService>
    {
        class When_creating_an_external_account
        {
            Because of = () => _result = Subject.CreateExternalAccount(LoginProvider, ExternalUserId, DisplayName, Email);

            It should_save_the_new_external_account =
                () => Injected<IExternalAccountsRepository>().AssertWasCalled(r => r.Save(_externalAccount));

            It should_return_the_new_external_account = () => _result.ShouldEqual(_externalAccount);

            Establish context = () =>
            {
                var accountRegistration = NewInstanceOf<IAccountRegistration>();
                Injected<IExternalAccountRegistrationFactory>().Stub(f => f.CreateItem(ExternalUserId, DisplayName, Email)).Return(accountRegistration);

                var accountRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                accountRegistrationResult.Success = true;
                accountRegistrationResult.Account = NewInstanceOf<Account>();
                Injected<IAccountService>().Stub(s => s.Register(accountRegistration)).Return(accountRegistrationResult);

                _externalAccount = NewInstanceOf<ExternalAccount>();
                Injected<INewExternalAccountProvider>().Stub(p => p.Provide(accountRegistrationResult.Account, LoginProvider, ExternalUserId)).Return(_externalAccount);
            };

            static ExternalAccount _result;
            static ExternalAccount _externalAccount;
            const string LoginProvider = "Provider";
            const string ExternalUserId = "User ID";
            const string DisplayName = "Display Name";
            const string Email = "email@emai.com";
        }
    }
}

