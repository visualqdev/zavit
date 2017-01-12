using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Registration;

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

            It should_set_the_account_type_to_be_the_same_as_registration_account_type =
                () => _result.AccountType.ShouldEqual(_accountRegistration.AccountType);

            It should_create_a_new_profile = () => _result.Profile.ShouldEqual(_profile);

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

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileCreator>().Stub(c => c.CreateProfile(_accountRegistration)).Return(_profile);
            };

            static IAccountRegistration _accountRegistration;
            static Account _result;
            static string HashedPassword = "Hashed Password";
            static Profile _profile;
        }

        class When_creating_an_external_account
        {
            Because of = () => _result = Subject.Create(_accountRegistration);

            It should_set_the_username_to_be_the_same_as_the_registration_username =
                () => _result.Username.ShouldEqual(_accountRegistration.Username);

            It should_set_the_password_to_be_null =
                () => _result.Password.ShouldBeNull();

            It should_set_the_account_type_to_be_the_same_as_registration_account_type =
                () => _result.AccountType.ShouldEqual(_accountRegistration.AccountType);

            It should_create_a_new_profile = () => _result.Profile.ShouldEqual(_profile);

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

                _profile = NewInstanceOf<Profile>();
                Injected<IProfileCreator>().Stub(c => c.CreateProfile(_accountRegistration)).Return(_profile);
            };

            static IAccountRegistration _accountRegistration;
            static Account _result;
            static string HashedPassword = "Hashed Password";
            static Profile _profile;
        }
    }
}

