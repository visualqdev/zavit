using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Accounts.Tests.Registrations 
{
    [Subject("AccountRegistrationResultFactory")]
    public class AccountRegistrationResultFactoryTests : TestOf<AccountRegistrationResultFactory>
    {
        class When_creating_a_successful_registraion_result
        {
            Because of = () => _result = Subject.CreateSuccessful(_account);

            It should_set_the_success_property_of_the_result_to_true = () => _result.Success.ShouldBeTrue();

            It should_leave_the_error_message_blank = () => _result.ErrorMessage.ShouldBeNull();

            It should_attach_the_provided_account_to_the_result = () => _result.Account.ShouldEqual(_account);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
            };

            static AccountRegistrationResult _result;
            static Account _account;
        }

        class When_creating_a_failed_registraion_result
        {
            Because of = () => _result = Subject.CreateFailed(ErrorMessage);

            It should_set_the_success_property_of_the_result_to_false = () => _result.Success.ShouldBeFalse();

            It should_set_the_error_message_to_be_the_provided_message = () => _result.ErrorMessage.ShouldEqual(ErrorMessage);

            It shold_leave_the_account_property_to_be_null = () => _result.Account.ShouldBeNull();

            static AccountRegistrationResult _result;
            const string ErrorMessage = "Error message";
        }
    }
}

