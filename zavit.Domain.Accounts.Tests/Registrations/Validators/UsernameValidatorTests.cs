using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Accounts.Registrations.Validators;

namespace zavit.Domain.Accounts.Tests.Registrations.Validators 
{
    [Subject("UsernameValidator")]
    public class UsernameValidatorTests : TestOf<UsernameValidator>
    {
        class When_validating_new_account_username_that_is_unique
        {
            Because of = () => _result = Subject.Validate(_accountRegistration);

            It should_return_null = () => _result.ShouldBeNull();

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();
                _accountRegistration.Stub(r => r.Password).Return("Password");

                Injected<IAccountRepository>()
                    .Stub(r => r.AccountExists(_accountRegistration.Password))
                    .Return(false);
            };

            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _result;
        }

        class When_validating_new_account_username_that_has_already_been_used
        {
            Because of = () => _result = Subject.Validate(_accountRegistration);

            It should_return_failed_result = () => _result.ShouldEqual(_accountRegistrationResult);

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();
                _accountRegistration.Stub(r => r.Username).Return("Username");

                Injected<IAccountRepository>()
                    .Stub(r => r.AccountExists(_accountRegistration.Username))
                    .Return(true);

                _accountRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                Injected<IAccountRegistrationResultFactory>()
                    .Stub(v => v.CreateFailed(UsernameValidator.UsernameTakenMessage))
                    .Return(_accountRegistrationResult);
            };

            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _result;
            static AccountRegistrationResult _accountRegistrationResult;
        }
    }
}

