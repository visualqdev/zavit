using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Profiles;
using zavit.Web.Api.DtoFactories.ProfileImages;
using zavit.Web.Api.Dtos.ProfileImages;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Tests.DtoFactories.ProfileImages 
{
    [Subject("ProfileImageUploadDtoFactory")]
    public class ProfileImageUploadDtoFactoryTests : TestOf<ProfileImageUploadDtoFactory>
    {
        class When_creating_profile_image_upload_dto
        {
            Because of = () => _result = Subject.CreateItem(_profile);

            It should_set_the_profile_image_url_to_the_url_provided_by_the_builder = () => _result.ProfileImageUrl.ShouldEqual(ProfileImageUrl);

            Establish context = () =>
            {
                _profile = NewInstanceOf<Profile>();

                Injected<IProfileImageUrlBuilder>().Stub(b => b.Build(_profile)).Return(ProfileImageUrl);
            };

            static Profile _profile;
            static ProfileImageUploadDto _result;
            const string ProfileImageUrl = "http://image/url";
        }
    }
}