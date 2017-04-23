using System.Collections.Generic;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Accounts.Registrations.Validators;
using zavit.Domain.Shared;

namespace zavit.Domain.Accounts.Tests 
{
    [Subject("AccountServiceTests")]
    public class AccountServiceTests : TestOf<AccountService>
    {
        class When_registering_a_new_account
        {
            Because of = () => _result = Subject.Register(_accountRegistration).Result;

            It should_store_the_new_account_in_the_repository =
                () => Injected<IAccountRepository>().AssertWasCalled(r => r.Save(_account));

            It should_return_a_success_regsitration_result = () => _result.ShouldEqual(_successRegistrationResult);

            It should_send_an_email_verification_mail =
                () => Injected<IVerifyEmailMailer>().AssertWasCalled(m => m.SendMail(_account));

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();

                _account = NewInstanceOf<Account>();
                Injected<IAccountCreator>().Stub(c => c.Create(_accountRegistration)).Return(Task.FromResult(_account));

                _successRegistrationResult = NewInstanceOf<AccountRegistrationResult>();
                Injected<IAccountRegistrationResultFactory>()
                    .Stub(f => f.CreateSuccessful(_account))
                    .Return(_successRegistrationResult);

                Injected<IVerifyEmailMailer>().Stub(m => m.SendMail(_account)).Return(Task.FromResult(0));
            };

            static AccountRegistrationResult _result;
            static IAccountRegistration _accountRegistration;
            static AccountRegistrationResult _successRegistrationResult;
            static Account _account;
        }

        class When_registering_a_new_account_that_is_not_passing_validation
        {
            Because of = () => _result = Subject.Register(_accountRegistration).Result;

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

        class When_verifying_an_account_and_the_provided_verification_code_is_valid
        {
            Because of = () => _result = Subject.VerifyAccount(VerificationCode);

            It should_verify_the_account = () => _account.AssertWasCalled(a => a.Verify(Injected<IDateTime>()));

            It should_save_the_verified_account =
                () => Injected<IAccountRepository>().AssertWasCalled(r => r.Save(_account));

            It should_return_true_to_indicate_account_has_been_verified = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();

                Injected<IAccountRepository>().Stub(r => r.GetByVerificationCode(VerificationCode)).Return(_account);
            };

            static bool _result;
            static Account _account;
            const string VerificationCode = "Verification Code";
        }

        class When_verifying_an_account_and_the_provided_verification_code_is_not_valid
        {
            Because of = () => _result = Subject.VerifyAccount(VerificationCode);

            It should_return_false_to_indicate_account_has_not_been_verified = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                Injected<IAccountRepository>().Stub(r => r.GetByVerificationCode(VerificationCode)).Return(null);
            };

            static bool _result;
            const string VerificationCode = "Verification Code";
        }
    }
}

