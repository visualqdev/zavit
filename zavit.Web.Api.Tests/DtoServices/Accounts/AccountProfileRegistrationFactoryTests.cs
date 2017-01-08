using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Accounts.Registrations;
using zavit.Domain.Profiles;
using zavit.Web.Api.Dtos.Accounts;
using zavit.Web.Api.DtoServices.Accounts;

namespace zavit.Web.Api.Tests.DtoServices.Accounts 
{
    [Subject("AccountProfileRegistrationFactory")]
    public class AccountProfileRegistrationFactoryTests : TestOf<AccountProfileRegistrationFactory>
    {
        class When_creating_account_profile_registration
        {
            Because of = () => _result = Subject.CreateItem(_accountRegistrationDto);

            It should_always_return_account_profile_registration_instance = () => _result.ShouldBeOfExactType<AccountProfileRegistration>();

            It should_set_gender_to_not_specified = () => _result.Gender.ShouldEqual(Gender.NotSpecified);

            It should_set_account_type_to_internal = () => _result.AccountType.ShouldEqual(AccountType.Internal);

            It should_set_the_email_to_be_the_same_as_the_dto_username = () => _result.Email.ShouldEqual(_accountRegistrationDto.Username);

            It should_set_the_username_to_be_the_same_as_the_dto = () => _result.Username.ShouldEqual(_accountRegistrationDto.Username);

            It should_set_the_password_to_be_the_same_as_the_dto = () => _result.Password.ShouldEqual(_accountRegistrationDto.Password);

            It should_set_the_display_name_to_be_the_same_as_the_dto = () => _result.DisplayName.ShouldEqual(_accountRegistrationDto.DisplayName);

            Establish context = () =>
            {
                _accountRegistrationDto = NewInstanceOf<AccountRegistrationDto>();
                _accountRegistrationDto.Username = "test@email.com";
                _accountRegistrationDto.Password = "password";
                _accountRegistrationDto.DisplayName = "Display Name";
            };

            static AccountRegistrationDto _accountRegistrationDto;
            static IAccountRegistration _result;
        }
    }
}

