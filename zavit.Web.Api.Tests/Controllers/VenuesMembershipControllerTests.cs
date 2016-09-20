using System.Web.Http;
using System.Web.Http.Results;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.DtoServices.VenueMemberships;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("VenuesMembershipController")]
    public class VenuesMembershipControllerTests : TestOf<VenueMembershipsController>
    {
        class When_adding_a_user_venue_for_a_user
        {
            Because of = () => _result = Subject.Post(_venueMembershipDto);

            It should_return_http_result_specifying_the_default_route =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueMembershipDto>)_result).RouteName.ShouldEqual(CommonRoutes.Default);

            It should_return_http_result_specifying_venues_controller_as_a_route_value =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueMembershipDto>)_result).RouteValues["controller"].ToString().ShouldEqual("venuememberships");

            It should_return_http_result_specifying_the_created_venue_id_as_a_route_value =
                () => int.Parse(((CreatedAtRouteNegotiatedContentResult<VenueMembershipDto>)_result).RouteValues["id"].ToString()).ShouldEqual(_createdVenueMembershipDto.VenueId);

            Establish context = () =>
            {
                _venueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                
                _createdVenueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                _createdVenueMembershipDto.VenueId = 123;
                Injected<IVenueMembershipDtoService>().Stub(s => s.AddVenueMembership(_venueMembershipDto)).Return(_createdVenueMembershipDto);
            };

            static VenueMembershipDto _venueMembershipDto;
            static VenueMembershipDto _createdVenueMembershipDto;
            static IHttpActionResult _result;
        }
    }
}

