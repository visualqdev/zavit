using System;
using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Registration;
using zavit.Domain.Shared;

namespace zavit.Domain.Accounts.Tests.Registrations 
{
    [Subject("AccountCreator")]
    public class AccountCreatorTests : TestOf<AccountCreator>
    {
        class When_creating_an_internal_account
        {
            Because of = () => _result = Subject.Create(_accountRegistration).Result;

            It should_set_the_username_to_be_the_same_as_the_registration_username = 
                () => _result.Username.ShouldEqual(_accountRegistration.Username);

            It should_set_the_password_to_be_the_hashed_password_with_salt =
                () => _result.Password.ShouldEqual(HashedPassword);

            It should_set_the_account_type_to_be_the_same_as_registration_account_type =
                () => _result.AccountType.ShouldEqual(_accountRegistration.AccountType);

            It should_create_a_new_profile = () => _result.Profile.ShouldEqual(_profile);

            It should_set_the_date_created_to_be_the_current_utc_date = () => _result.DateCreated.ShouldEqual(DateCreated);

            It should_set_the_account_verification_code_to_be_a_random_guid_string =
                () => _result.VerificationCode.ShouldEqual(VerificationCode);

            It should_keep_date_verified_to_be_null = () => _result.DateVerified.ShouldBeNull();

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
                Injected<IProfileCreator>().Stub(c => c.CreateProfile(_accountRegistration)).Return(Task.FromResult(_profile));

                Injected<IDateTime>().Stub(d => d.UtcNow).Return(DateCreated);
                Injected<IGuid>().Stub(g => g.NewGuidString()).Return(VerificationCode);
            };

            static IAccountRegistration _accountRegistration;
            static Account _result;
            static string HashedPassword = "Hashed Password";
            static Profile _profile;
            static readonly DateTime DateCreated = new DateTime(2017, 04, 20, 22, 25, 0);
            const string VerificationCode = "VerificationCode";
        }

        class When_creating_an_external_account
        {
            Because of = () => _result = Subject.Create(_accountRegistration).Result;

            It should_set_the_username_to_be_the_same_as_the_registration_username =
                () => _result.Username.ShouldEqual(_accountRegistration.Username);

            It should_set_the_password_to_be_null =
                () => _result.Password.ShouldBeNull();

            It should_set_the_account_type_to_be_the_same_as_registration_account_type =
                () => _result.AccountType.ShouldEqual(_accountRegistration.AccountType);

            It should_create_a_new_profile = () => _result.Profile.ShouldEqual(_profile);

            It should_set_the_date_created_to_be_the_current_utc_date = () => _result.DateCreated.ShouldEqual(DateCreated);

            It should_set_the_verified_date_to_be_same_as_date_created = () => _result.DateVerified.ShouldEqual(DateCreated);

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
                Injected<IDateTime>().Stub(d => d.UtcNow).Return(DateCreated);
                Injected<IProfileCreator>().Stub(c => c.CreateProfile(_accountRegistration)).Return(Task.FromResult(_profile));
            };

            static IAccountRegistration _accountRegistration;
            static Account _result;
            static string HashedPassword = "Hashed Password";
            static readonly DateTime DateCreated = new DateTime(2017, 04, 20, 22, 25, 0);
            static Profile _profile;
        }
    }
}

