using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.VenueMemberships;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.VenueMemberships;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("VenueMembershipsController")]
    public class VenueMembershipsControllerTests : TestOf<VenueMembershipsController>
    {
        class When_adding_a_user_venue_for_a_user
        {
            Because of = () => _result = Subject.Post(_venueMembershipDto);

            It should_return_http_result_specifying_the_default_route =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueMembershipDto>)_result).RouteName.ShouldEqual(CommonRoutes.Default);

            It should_return_http_result_specifying_venues_controller_as_a_route_value =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueMembershipDto>)_result).RouteValues["controller"].ToString().ShouldEqual("venuememberships");

            It should_return_http_result_specifying_the_created_venue_id_as_a_route_value =
                () => int.Parse(((CreatedAtRouteNegotiatedContentResult<VenueMembershipDto>)_result).RouteValues["id"].ToString()).ShouldEqual(_createdVenueMembershipDto.Venue.Id);

            Establish context = () =>
            {
                _venueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                
                _createdVenueMembershipDto = NewInstanceOf<VenueMembershipDto>();
                _createdVenueMembershipDto.Venue = NewInstanceOf<MembershipVenueDto>();
                _createdVenueMembershipDto.Venue.Id = 123;
                Injected<IVenueMembershipDtoService>().Stub(s => s.AddVenueMembership(_venueMembershipDto)).Return(_createdVenueMembershipDto);
            };

            static VenueMembershipDto _venueMembershipDto;
            static VenueMembershipDto _createdVenueMembershipDto;
            static IHttpActionResult _result;
        }

        class When_getting_memberships
        {
            Because of = () => _result = Subject.Get();

            It should_return_venue_memberships_from_venue_membership_dto_service = () => _result.ShouldEqual(_membershipDtos);

            Establish context = () =>
            {
                _membershipDtos = new[] { NewInstanceOf<VenueMembershipDto>() };
                Injected<IVenueMembershipDtoService>().Stub(s => s.GetVenueMemberships()).Return(_membershipDtos);
            };

            static IEnumerable<VenueMembershipDto> _result;
            static IEnumerable<VenueMembershipDto> _membershipDtos;
        }

        class When_getting_a_venue_membership
        {
            Because of = () => _result = Subject.Get(_venueId);

            It should_return_the_venue_membership_details_dto = () => _result.ShouldEqual(_venueMembershipDetailsDto);

            Establish context = () =>
            {
                _venueMembershipDetailsDto = NewInstanceOf<VenueMembershipDetailsDto>();
                Injected<IVenueMembershipDetailsDtoService>()
                    .Stub(s => s.GetMembershipDetails(_venueId))
                    .Return(_venueMembershipDetailsDto);
            };

            const int _venueId = 1;
            static VenueMembershipDetailsDto _result;
            static VenueMembershipDetailsDto _venueMembershipDetailsDto;
        }
    }
}

