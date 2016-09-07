using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;

namespace zavit.Domain.Accounts.Tests.Registrations 
{
    [Subject("AccountCreator")]
    public class AccountCreatorTests : TestOf<AccountCreator>
    {
        class When_creating_an_internal_account
        {
            Because of = () => _result = Subject.Create(_accountRegistration);

            It should_set_the_username_to_be_the_same_as_the_registration_username = 
                () => _result.Username.ShouldEqual(_accountRegistration.Username);

            It should_set_the_password_to_be_the_hashed_password_with_salt =
                () => _result.Password.ShouldEqual(HashedPassword);

            It should_set_the_display_name_to_be_the_same_as_registration_display_name =
                () => _result.DisplayName.ShouldEqual(_accountRegistration.DisplayName);

            It should_set_the_email_to_be_the_same_as_registration_email =
                () => _result.Email.ShouldEqual(_accountRegistration.Email);

            It should_set_the_account_type_to_be_the_same_as_registration_account_type =
                () => _result.AccountType.ShouldEqual(_accountRegistration.AccountType);

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();
                _accountRegistration.Stub(r => r.Password).Return("Password");
                _accountRegistration.Stub(r => r.Username).Return("User name");
                _accountRegistration.Stub(r => r.DisplayName).Return("Display name");
                _accountRegistration.Stub(r => r.Email).Return("Email");
                _accountRegistration.Stub(r => r.AccountType).Return(AccountType.Internal);

                Injected<IAccountSecurity>()
                    .Stub(h => h.HashPassword(_accountRegistration.Password))
                    .Return(HashedPassword);
            };

            static IAccountRegistration _accountRegistration;
            static Account _result;
            static string HashedPassword = "Hashed Password";
        }

        class When_creating_an_external_account
        {
            Because of = () => _result = Subject.Create(_accountRegistration);

            It should_set_the_username_to_be_the_same_as_the_registration_username =
                () => _result.Username.ShouldEqual(_accountRegistration.Username);

            It should_set_the_password_to_be_null =
                () => _result.Password.ShouldBeNull();

            It should_set_the_display_name_to_be_the_same_as_registration_display_name =
                () => _result.DisplayName.ShouldEqual(_accountRegistration.DisplayName);

            It should_set_the_email_to_be_the_same_as_registration_email =
                () => _result.Email.ShouldEqual(_accountRegistration.Email);

            It should_set_the_account_type_to_be_the_same_as_registration_account_type =
                () => _result.AccountType.ShouldEqual(_accountRegistration.AccountType);

            Establish context = () =>
            {
                _accountRegistration = NewInstanceOf<IAccountRegistration>();
                _accountRegistration.Stub(r => r.Username).Return("User name");
                _accountRegistration.Stub(r => r.DisplayName).Return("Display name");
                _accountRegistration.Stub(r => r.Email).Return("Email");
                _accountRegistration.Stub(r => r.AccountType).Return(AccountType.External);

                Injected<IAccountSecurity>()
                    .Stub(h => h.HashPassword(_accountRegistration.Password))
                    .Return(HashedPassword);
            };

            static IAccountRegistration _accountRegistration;
            static Account _result;
            static string HashedPassword = "Hashed Password";
        }
    }
}

