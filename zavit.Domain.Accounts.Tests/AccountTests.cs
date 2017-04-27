using System;
using Machine.Specifications;
using Machine.Specifications.Model;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Shared;

namespace zavit.Domain.Accounts.Tests 
{
    [Subject("Account")]
    public class AccountTests : TestOf<Account>
    {
        class When_verifying_a_password_that_is_valid
        {
            Because of = () => _result = Subject.VerifyPassword(Password, _accountSecurity);

            It should_return_true_to_indicate_password_has_been_verified = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                Subject.Password = "Hashes Password";

                _accountSecurity = NewInstanceOf<IAccountSecurity>();
                _accountSecurity.Stub(s => s.ValidatePassword(Password, Subject.Password)).Return(true);
            };

            static bool _result;
            static IAccountSecurity _accountSecurity;
            const string Password = "Test Password";
        }

        class When_verifying_a_password_that_is_invalid
        {
            Because of = () => _result = Subject.VerifyPassword(Password, _accountSecurity);

            It should_return_false_to_indicate_password_could_not_been_verified = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                Subject.Password = "Hashes Password";

                _accountSecurity = NewInstanceOf<IAccountSecurity>();
                _accountSecurity.Stub(s => s.ValidatePassword(Password, Subject.Password)).Return(false);
            };

            static bool _result;
            static IAccountSecurity _accountSecurity;
            const string Password = "Test Password";
        }

        class When_verifying_a_password_for_an_account_that_uses_external_authentication
        {
            Because of = () => _result = Subject.VerifyPassword(Password, _accountSecurity);

            It should_not_use_account_security_to_try_to_verify_password =
                () => _accountSecurity.AssertWasNotCalled(s => s.ValidatePassword(Arg<string>.Is.Anything, Arg<string>.Is.Anything));

            It should_return_false_to_indicate_password_could_not_been_verified = () => _result.ShouldBeFalse();

            Establish context = () =>
            {
                Subject.AccountType = AccountType.External;
                _accountSecurity = NewInstanceOf<IAccountSecurity>();
            };

            static bool _result;
            static IAccountSecurity _accountSecurity;
            const string Password = "Test Password";
        }

        class When_checking_if_account_needs_verification_and_verification_code_is_not_set
        {
            Because of = () => _result = Subject.NeedsVerification;

            It should_return_false = () => _result.ShouldBeFalse();
            
            static bool _result;
        }

        class When_checking_if_account_needs_verification_and_verification_code_is_set
        {
            Because of = () => _result = Subject.NeedsVerification;

            It should_return_false = () => _result.ShouldBeTrue();

            Establish context = () =>
            {
                Subject.VerificationCode = "Test Code";
            };

            static bool _result;
        }

        class When_verifying_account
        {
            Because of = () => Subject.Verify(_dateTime);

            It should_set_verification_code_to_null = () => Subject.VerificationCode.ShouldBeNull();

            It should_set_the_date_verified_to_be_the_current_date = 
                () => Subject.DateVerified.ShouldEqual(CurrentDate);

            Establish context = () =>
            {
                _dateTime = NewInstanceOf<IDateTime>();
                _dateTime.Stub(d => d.UtcNow).Return(CurrentDate);
            };

            static IDateTime _dateTime;
            static readonly DateTime CurrentDate = new DateTime(2017, 4, 23, 20, 18, 0);
        }
    }
}

