using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Accounts.Registrations.Validators;

namespace zavit.Domain.Accounts.Tests.Registrations.Validators 
{
    [Subject("PasswordValidator")]
    public class PasswordValidatorTests : TestOf<PasswordValidator>
    {
        class When_validating_account_registration_password_that_is_6_characters_long
        {
            Because of = () => _result = Subject.Validate(_accountRegistration);

            It should_return_null = () => _result.ShouldBeNull();

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();
                _accountRegistration.Stub(r => r.Password).Return("123456");
                _accountRegistration.Stub(r => r.AccountType).Return(AccountType.Internal);
            };

            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _result;
        }

        class When_validating_account_registration_password_that_is_shorter_than_6_characters
        {
            Because of = () => _result = Subject.Validate(_accountRegistration);

            It should_return_failed_result = () => _result.ShouldEqual(_accountRegistrationResult);

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();
                _accountRegistration.Stub(r => r.Password).Return("12345");
                _accountRegistration.Stub(r => r.AccountType).Return(AccountType.Internal);

                _accountRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                Injected<IAccountRegistrationResultFactory>()
                    .Stub(v => v.CreateFailed(PasswordValidator.PasswordTooShort))
                    .Return(_accountRegistrationResult);
            };

            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _result;
            static AccountRegistrationResult _accountRegistrationResult;
        }

        class When_validating_account_registration_password_for_an_external_account
        {
            Because of = () => _result = Subject.Validate(_accountRegistration);

            It should_not_try_to_call_account_registration_factory =
                () => Injected<IAccountRegistrationResultFactory>().AssertWasNotCalled(f => f.CreateFailed(Arg<string>.Is.Anything));

            It should_always_return_null = () => _result.ShouldBeNull();

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();
                _accountRegistration.Stub(r => r.AccountType).Return(AccountType.External);
            };

            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _result;
        }
    }
}

