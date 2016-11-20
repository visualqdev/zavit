using System.Collections.Generic;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Activities;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Tests.DtoFactories.Venues
{
    [Subject("VenueDtoFactory")]
    public class VenueDtoFactoryTests : TestOf<VenueDtoFactory>
    {
        class When_creating_a_new_dto
        {
            Because of = () => _result = Subject.Create(_venue);

            It should_set_the_dto_id_to_be_the_same_as_venue_id = () => _result.Id.ShouldEqual(_venue.Id);

            It should_set_the_name_to_be_the_same_as_the_name_of_the_venue = 
                () => _result.Name.ShouldEqual(_venue.Name);

            It should_set_the_latitude_to_be_the_same_as_that_of_the_venue = 
                () => _result.Latitude.ShouldEqual(_venue.Latitude);

            It should_set_the_longitude_to_be_the_same_as_that_of_the_venue =
                () => _result.Longitude.ShouldEqual(_venue.Longitude);

            It should_set_the_public_place_id_to_be_the_same_as_that_of_the_venue =
                () => _result.PublicPlaceId.ShouldEqual(_venue.PublicPlaceId);

            It should_set_the_address_to_be_the_same_as_that_of_the_venue =
                () => _result.Address.ShouldEqual(_venue.Address);

            It should_create_activity_dto_for_every_activity_available_in_venue =
                () => _result.Activities.ShouldContainOnly(_activityDto, _otherActivityDto);

            Establish context = () =>
            {
                _venue = NewInstanceOf<IVenue>();
                _venue.Stub(v => v.Id).Return(1234);
                _venue.Stub(v => v.Name).Return("Venue name");
                _venue.Stub(v => v.Address).Return("Venue address");
                _venue.Stub(v => v.Latitude).Return(-0.5);
                _venue.Stub(v => v.Longitude).Return(1.5);
                _venue.Stub(v => v.PublicPlaceId).Return("PublicPlaceID");

                var activity = NewInstanceOf<Activity>();
                var otherActivity = NewInstanceOf<Activity>();
                _venue.Stub(v => v.Activities).Return(new List<Activity> { activity, otherActivity });

                _activityDto = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(activity)).Return(_activityDto);

                _otherActivityDto = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(otherActivity)).Return(_otherActivityDto);
            };

            static IVenue _venue;
            static VenueDto _result;
            static VenueActivityDto _activityDto;
            static VenueActivityDto _otherActivityDto;
        }
    }
}