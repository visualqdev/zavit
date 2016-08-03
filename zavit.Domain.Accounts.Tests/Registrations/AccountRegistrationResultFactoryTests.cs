using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Accounts.Tests.Registrations 
{
    [Subject("AccountRegistrationResultFactory")]
    public class AccountRegistrationResultFactoryTests : TestOf<AccountRegistrationResultFactory>
    {
        class When_creating_a_successful_registraion_result
        {
            Because of = () => _result = Subject.CreateSuccessful();

            It should_set_the_success_property_of_the_result_to_true = () => _result.Success.ShouldBeTrue();

            It should_leave_the_error_message_blank = () => _result.ErrorMessage.ShouldBeNull();

            static AccountRegistrationResult _result;
        }

        class When_creating_a_failed_registraion_result
        {
            Because of = () => _result = Subject.CreateFailed(ErrorMessage);

            It should_set_the_success_property_of_the_result_to_false = () => _result.Success.ShouldBeFalse();

            It should_set_the_error_message_to_be_the_provided_message = () => _result.ErrorMessage.ShouldEqual(ErrorMessage);

            static AccountRegistrationResult _result;
            const string ErrorMessage = "Error message";
        }
    }
}

