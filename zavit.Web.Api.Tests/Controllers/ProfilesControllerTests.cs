using System.Web.Http;
using System.Web.Http.Results;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.Profiles;
using zavit.Web.Api.DtoServices.Profiles;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("ProfilesController")]
    public class ProfilesControllerTests : TestOf<ProfilesController>
    {
        class When_updating_profile
        {
            Because of = () => _result = Subject.Post(_profiledDto);

            It should_return_http_result_specifying_the_default_route =
               () => ((CreatedAtRouteNegotiatedContentResult<ProfileDto>)_result).RouteName.ShouldEqual(ProfilesController.GetMyProfileRoute);

            It should_return_http_result_with_a_newly_created_message_thread_dto =
                 () => ((CreatedAtRouteNegotiatedContentResult<ProfileDto>)_result).Content.ShouldEqual(_updateProfileDto);

            Establish context = () =>
            {
                _profiledDto = NewInstanceOf<ProfileDto>();
                _updateProfileDto = NewInstanceOf<ProfileDto>();

                Injected<IProfileDtoService>().Stub(p => p.Update(_profiledDto)).Return(_updateProfileDto);
            };

            static IHttpActionResult _result;
            static ProfileDto _profiledDto;
            static ProfileDto _updateProfileDto;
        }

        class When_getting_my_profile
        {
            Because of = () => _result = Subject.GetMyProfile();

            It should_return_profile_dto = () => _result.ShouldEqual(_profileDto);

            Establish context = () =>
            {
                _profileDto = NewInstanceOf<ProfileDto>();
                Injected<IProfileDtoService>().Stub(s => s.GetMyProfile()).Return(_profileDto);
            };

            static ProfileDto _result;
            static ProfileDto _profileDto;
        }
    }
}

