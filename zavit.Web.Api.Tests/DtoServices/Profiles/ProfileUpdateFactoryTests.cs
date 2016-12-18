using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Updating;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;
using zavit.Web.Core.Context;

namespace zavit.Web.Api.Tests.DtoServices.Profiles 
{
    [Subject("ProfileUpdateFactory")]
    public class ProfileUpdateFactoryTests : TestOf<ProfileUpdateFactory>
    {
        class When_creating_a_profile_update
        {
            Because of = () => _result = Subject.CreateItem(_profileDto);

            It should_set_the_account_to_the_current_account = () => _result.Account.ShouldEqual(_account);

            It should_set_the_display_name_to_be_same_as_the_profile_dto =
                () => _result.DisplayName.ShouldEqual(_profileDto.DisplayName);

            It should_set_the_email_address_to_be_same_as_the_profile_dto =
               () => _result.Email.ShouldEqual(_profileDto.Email);

            It should_set_the_gender_to_be_the_same_as_the_profile_dto =
                () => _result.Gender.ShouldEqual(_profileDto.Gender);

            It should_set_the_about_property_to_be_the_same_as_the_profile_dto =
                () => _result.About.ShouldEqual(_profileDto.About);

            Establish context = () =>
            {
                _account = NewInstanceOf<Account>();
                Injected<IUserContext>().Stub(c => c.Account).Return(_account);

                _profileDto = NewInstanceOf<ProfileDto>();
                _profileDto.DisplayName = "Test display name";
                _profileDto.Gender = Gender.Female;
                _profileDto.About = "This is my bio";
                _profileDto.Email = "test@email.com";
            };

            static ProfileDto _profileDto;
            static ProfileUpdate _result;
            static Account _account;
        }
    }
}

