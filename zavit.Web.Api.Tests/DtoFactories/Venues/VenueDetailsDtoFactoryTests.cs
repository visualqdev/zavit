using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Activities;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Tests.DtoFactories.Venues 
{
    [Subject("VenueDetailsDtoFactory")]
    public class VenueDetailsDtoFactoryTests : TestOf<VenueDetailsDtoFactory>
    {
        class When_creating_a_venue_details_dto
        {
            Because of = () => _result = Subject.Create(_venue);

            It should_set_the_id_to_be_the_venue_id = () => _result.Id.ShouldEqual(_venue.Id);

            It should_set_the_name_to_be_the_venue_name = () => _result.Name.ShouldEqual(_venue.Name);

            It should_set_the_address_to_be_the_venue_address = () => _result.Address.ShouldEqual(_venue.Address);

            It should_add_an_activity_dto_for_each_venue_activity =
                () => _result.Activities.ShouldContainOnly(_venueActivityDto);

            It should_set_the_longitude_to_be_the_venue_longitude =
                () => _result.Longitude.ShouldEqual(_venue.Longitude);

            It should_set_the_latitude_to_be_the_venue_latitude =
                () => _result.Latitude.ShouldEqual(_venue.Latitude);

            Establish context = () =>
            {
                _venue = NewInstanceOf<Venue>();
                _venue.Name = "Test name";
                _venue.Id = 123;
                _venue.Address = "Test address";
                _venue.Longitude = 0.1;
                _venue.Latitude = -0.2;

                var activity = NewInstanceOf<Activity>();
                _venue.Activities = new[] { activity };

                _venueActivityDto = NewInstanceOf<VenueActivityDto>();
                Injected<IVenueActivityDtoFactory>().Stub(f => f.CreateItem(activity)).Return(_venueActivityDto);
            };

            static VenueDetailsDto _result;
            static Venue _venue;
            static VenueActivityDto _venueActivityDto;
        }
    }
}

