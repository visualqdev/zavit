using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Accounts.Registrations.Validators;

namespace zavit.Domain.Accounts.Tests 
{
    [Subject("AccountServiceTests")]
    public class AccountServiceTests : TestOf<AccountService>
    {
        class When_registering_a_new_account
        {
            Because of = () => _result = Subject.Register(_accountRegistration);

            It should_store_the_new_account_in_the_repository =
                () => Injected<IAccountRepository>().AssertWasCalled(r => r.Save(_account));

            It should_return_a_success_regsitration_result = () => _result.ShouldEqual(_successRegistrationResult);

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();

                _account = NewInstanceOf<Account>();
                Injected<IAccountCreator>().Stub(c => c.Create(_accountRegistration)).Return(_account);

                _successRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                Injected<IAccountRegistrationResultFactory>()
                    .Stub(f => f.CreateSuccessful(_account))
                    .Return(_successRegistrationResult);
            };

            static AccountRegistrationResult _result;
            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _successRegistrationResult;
            static Account _account;
        }

        class When_registering_a_new_account_that_is_not_passing_validation
        {
            Because of = () => _result = Subject.Register(_accountRegistration);

            It should_return_an_error_result_provided_by_validator = () => _result.ShouldEqual(_errorRegistrationResult);

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();

                _errorRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                var validators = (List<IAccountRegistrationValidator>)Injected<IEnumerable<IAccountRegistrationValidator>>();
                var validator = NewInstanceOf<IAccountRegistrationValidator>();
                validator.Stub(v => v.Validate(_accountRegistration)).Return(_errorRegistrationResult);
                validators.Add(validator);
            };

            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _result;
            static AccountRegistrationResult _errorRegistrationResult;
        }
    }
}

