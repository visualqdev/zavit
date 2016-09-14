using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.ExternalAccounts.Registrations;

namespace zavit.Domain.ExternalAccounts.Tests.Registrations 
{
    [Subject("ExternalAccountRegistrationFactory")]
    public class ExternalAccountRegistrationFactoryTests : TestOf<ExternalAccountRegistrationFactory>
    {
        class When_creating_an_account_registration
        {
            Because of = () => _result = Subject.CreateItem(Username, DisplayName, Email);

            It should_set_the_account_registration_username = () => _result.Username.ShouldEqual(Username);

            It should_set_the_account_registration_email = () => _result.Email.ShouldEqual(Email);

            It should_set_the_account_registration_display_name = () => _result.DisplayName.ShouldEqual(DisplayName);

            static IAccountRegistration _result;
            const string Username = "Username";
            const string DisplayName = "Display name";
            const string Email = "Email";
        }
    }
}

