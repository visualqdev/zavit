using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Accounts;
using zavit.Domain.Profiles;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Tests.DtoServices.Profiles 
{
    [Subject("ProfileImageUrlBuilder")]
    public class ProfileImageUrlBuilderTests : TestOf<ProfileImageUrlBuilder>
    {
        class When_creating_profile_image_url
        {
            Because of = () => _result = Subject.Build(_profile);

            It should_return_url_to_profile_image_for_the_profile_account = () => _result.ShouldEqual("/api/profileimages/1234");

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
                _profile.ProfileImage = NewInstanceOf<ProfileImage>();
                _profile.ProfileImage.Id = 1234;
            };

            static Profile _profile;
            static string _result;
        }

        class When_creating_profile_image_url_but_profile_image_does_not_exist
        {
            Because of = () => _result = Subject.Build(_profile);

            It should_return_null = () => _result.ShouldBeNull();

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();
            };

            static Profile _profile;
            static string _result;
        }
    }
}

