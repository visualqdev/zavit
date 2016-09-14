using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Web.Api.Controllers;
using zavit.Web.Api.Dtos.Venues;
using zavit.Web.Api.DtoServices.Venues;

namespace zavit.Web.Api.Tests.Controllers 
{
    [Subject("VenuesControllerTests")]
    public class VenuesControllerTests : TestOf<VenuesController>
    {
        class When_posting_a_new_venue_dto
        {
            Because of = () => _result = Subject.Post(_venueDto, PlaceId).Result;

            It should_return_http_result_specifying_the_default_route =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueDto>) _result).RouteName.ShouldEqual(CommonRoutes.Default);

            It should_return_http_result_specifying_venues_controller_as_a_route_value =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueDto>)_result).RouteValues["controller"].ToString().ShouldEqual("venues");

            It should_return_http_result_specifying_the_created_venue_id_as_a_route_value =
                () => int.Parse(((CreatedAtRouteNegotiatedContentResult<VenueDto>)_result).RouteValues["id"].ToString()).ShouldEqual(_createdVenue.Id);

            Establish context = () =>
            {
                _venueDto = NewInstanceOf<VenueDto>();
                _createdVenue = NewInstanceOf<VenueDto>();
                _createdVenue.Id = 123;
                Injected<IVenueDtoService>().Stub(s => s.AddVenue(_venueDto, PlaceId)).Return(Task.FromResult(_createdVenue));
            };

            static VenueDto _createdVenue;
            static VenueDto _venueDto;
            static IHttpActionResult _result;
            const string PlaceId = "Place ID";
        }

        class When_getting_a_venue_for_a_place
        {
            Because of = () => _result = Subject.GetDefault(PlaceId).Result;

            It should_return_a_default_venue_details_dto = () => _result.ShouldEqual(_defaultVenueDetailsDto);

            Establish context = () =>
            {
                _defaultVenueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDtoService>().Stub(s => s.GetDefaultVenue(PlaceId)).Return(Task.FromResult(_defaultVenueDetailsDto));
            };

            static string PlaceId = "Place ID";
            static VenueDetailsDto _result;
            static VenueDetailsDto _defaultVenueDetailsDto;
        }
    }
}

