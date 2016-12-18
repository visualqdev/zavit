using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Web.Api.DtoFactories.Profiles;
using zavit.Web.Api.Dtos.Profiles;

namespace zavit.Web.Api.Tests.DtoFactories.Profiles 
{
    [Subject("ProfileDtoFactory")]
    public class ProfileDtoFactoryTests : TestOf<ProfileDtoFactory>
    {
        class When_creating_profile_dto
        {
            Because of = () => _result = Subject.CreateItem(_profile);

            It should_set_the_display_name_to_be_same_as_the_account = 
                () => _result.DisplayName.ShouldEqual(_profile.Account.DisplayName);

            It should_set_the_email_address_to_be_same_as_the_account =
                () => _result.Email.ShouldEqual(_profile.Account.Email);

            It should_set_the_gender_to_be_same_as_the_profile =
                () => _result.Gender.ShouldEqual(_profile.Gender);

            It should_set_the_about_property_to_be_same_as_the_profile =
               () => _result.About.ShouldEqual(_profile.About);

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.Gender = Gender.Male;

                _profile.Account = NewInstanceOf<Account>();
                _profile.Account.DisplayName = "Display Name";
                _profile.Account.Email = "test@email.com";
                _profile.About = "This is my bio";
            };

            static Profile _profile;
            static ProfileDto _result;
        }
    }
}

