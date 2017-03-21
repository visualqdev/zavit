using System.Threading.Tasks;
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
            Because of = () => _result = Subject.CreateExternalAccount(_accountRegistration).Result;

            It should_save_the_new_external_account =
                () => Injected<IExternalAccountsRepository>().AssertWasCalled(r => r.Save(_externalAccount));

            It should_return_the_new_external_account = () => _result.ShouldEqual(_externalAccount);

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<ExternalAccountRegistration>();
                _accountRegistration.Provider = "Provider";
                _accountRegistration.Username = "User Name";

                var accountRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                accountRegistrationResult.Success = true;
                accountRegistrationResult.Account = NewInstanceOf<Account>();
                Injected<IAccountService>().Stub(s => s.Register(_accountRegistration)).Return(Task.FromResult(accountRegistrationResult));

                _externalAccount = NewInstanceOf<ExternalAccount>();
                Injected<INewExternalAccountProvider>().Stub(p => p.Provide(accountRegistrationResult.Account, _accountRegistration.Provider, _accountRegistration.Username)).Return(_externalAccount);
            };

            static ExternalAccount _result;
            static ExternalAccount _externalAccount;
            static ExternalAccountRegistration _accountRegistration;
        }
    }
}

