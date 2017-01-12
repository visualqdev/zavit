using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles;
using zavit.Domain.Profiles.Updating;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Tests.DtoServices.Profiles 
{
    [Subject("ProfileUpdateFactory")]
    public class ProfileUpdateFactoryTests : TestOf<ProfileUpdateFactory>
    {
        class When_creating_a_profile_update
        {
            Because of = () => _result = Subject.CreateItem(_profileDto);

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
                _profileDto = NewInstanceOf<ProfileDto>();
                _profileDto.DisplayName = "Test display name";
                _profileDto.Gender = Gender.Female;
                _profileDto.About = "This is my bio";
                _profileDto.Email = "test@email.com";
            };

            static ProfileDto _profileDto;
            static ProfileUpdate _result;
        }
    }
}

