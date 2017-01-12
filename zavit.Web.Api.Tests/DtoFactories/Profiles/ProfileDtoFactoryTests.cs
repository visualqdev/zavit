using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles;
using zavit.Web.Api.DtoFactories.Profiles;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Tests.DtoFactories.Profiles 
{
    [Subject("ProfileDtoFactory")]
    public class ProfileDtoFactoryTests : TestOf<ProfileDtoFactory>
    {
        class When_creating_profile_dto
        {
            Because of = () => _result = Subject.CreateItem(_profile, AccountId);

            It should_set_the_display_name_to_be_same_as_the_account = 
                () => _result.DisplayName.ShouldEqual(_profile.DisplayName);

            It should_set_the_email_address_to_be_same_as_the_account =
                () => _result.Email.ShouldEqual(_profile.Email);

            It should_set_the_gender_to_be_same_as_the_profile =
                () => _result.Gender.ShouldEqual(_profile.Gender);

            It should_set_the_about_property_to_be_same_as_the_profile =
               () => _result.About.ShouldEqual(_profile.About);

            It should_set_the_id_to_be_the_profile_id = () => _result.AccountId.ShouldEqual(AccountId);

            It should_set_the_profile_image_url_to_be_url_provided_by_builder = () => _result.ProfileImageUrl.ShouldEqual(ProfileImageUrl);

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Gender = Gender.Male;

                _profile.DisplayName = "Display Name";
                _profile.Email = "test@email.com";
                _profile.About = "This is my bio";

                Injected<IProfileImageUrlBuilder>().Stub(b => b.Build(_profile)).Return(ProfileImageUrl);
            };

            const string ProfileImageUrl = "/profile/image/url";
            static Profile _profile;
            static ProfileDto _result;
            const int AccountId = 123456;
        }
    }
}

