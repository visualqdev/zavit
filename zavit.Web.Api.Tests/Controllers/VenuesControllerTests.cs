﻿using System.Collections.Generic;
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
        class When_getting_list_of_venues
        {
            Because of = () => _result = Subject.Get(_venueSearchCriteriaDto).Result;

            It should_return_a_list_of_places = () => _result.ShouldNotBeNull();

            Establish context = () =>
            {
                _venueSearchCriteriaDto = NewInstanceOf<VenueSearchCriteriaDto>();

                _venues = new[] { NewInstanceOf<VenueDto>() };
                Injected<IVenueDtoService>()
                    .Stub(s => s.SuggestVenues(_venueSearchCriteriaDto))
                    .Return(Task.FromResult(_venues));
            };

            static IEnumerable<VenueDto> _result;
            static IEnumerable<VenueDto> _venues;
            static VenueSearchCriteriaDto _venueSearchCriteriaDto;
        }

        class When_posting_a_new_venue_dto
        {
            Because of = () => _result = Subject.Post(_venueDetailsDto).Result;

            It should_return_http_result_specifying_the_default_route =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueDetailsDto>) _result).RouteName.ShouldEqual(CommonRoutes.Default);

            It should_return_http_result_specifying_venues_controller_as_a_route_value =
                () => ((CreatedAtRouteNegotiatedContentResult<VenueDetailsDto>)_result).RouteValues["controller"].ToString().ShouldEqual("venues");

            It should_return_http_result_specifying_the_created_venue_id_as_a_route_value =
                () => int.Parse(((CreatedAtRouteNegotiatedContentResult<VenueDetailsDto>)_result).RouteValues["id"].ToString()).ShouldEqual(_createdVenueDetailsDto.Id);

            Establish context = () =>
            {
                _venueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                _createdVenueDetailsDto = NewInstanceOf<VenueDetailsDto>();
                _createdVenueDetailsDto.Id = 123;
                Injected<IVenueDtoService>().Stub(s => s.AddVenue(_venueDetailsDto)).Return(Task.FromResult(_createdVenueDetailsDto));
            };

            static VenueDetailsDto _createdVenueDetailsDto;
            static VenueDetailsDto _venueDetailsDto;
            static IHttpActionResult _result;
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

        class When_getting_a_venue
        {
            Because of = () => _result = Subject.GetVenue(VenueId);

            It should_return_a_venue_provided_by_venue_dto_service = () => _result.ShouldEqual(_venueDto);

            Establish context = () =>
            {
                _venueDto = NewInstanceOf<VenueDetailsDto>();
                Injected<IVenueDtoService>().Stub(s => s.GetVenue(VenueId)).Return(_venueDto);
            };

            static int VenueId = 123;
            static VenueDetailsDto _result;
            static VenueDetailsDto _venueDto;
        }
    }
}

