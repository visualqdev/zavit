using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using Rhino.Mspec.Contrib.Extensions;
using zavit.Domain.Places;
using zavit.Domain.Venues;
using zavit.Web.Api.DtoFactories.Places;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Places;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.Tests.DtoFactories.Places
{
    [Subject("PlaceDtoFactory")]
    public class PlaceDtoFactoryTests : TestOf<PlaceDtoFactory>
    {
        class When_creating_a_place_dto
        {
            Because of = () => _result = Subject.CreateItem(_place);

            It should_set_the_longitude_to_be_the_same_as_that_of_the_place = () => _result.Longitude.ShouldEqual(_place.Longitude);

            It should_set_the_latitude_to_be_the_same_as_that_of_the_place = () => _result.Latitude.ShouldEqual(_place.Latitude);

            It should_set_the_place_id_to_be_the_same_as_that_of_the_place = () => _result.PlaceId.ShouldEqual(_place.PlaceId);

            It should_set_the_name_to_be_the_same_as_that_of_the_place = () => _result.Name.ShouldEqual(_place.Name);

            It should_set_the_address_to_be_the_same_as_that_of_the_place = () => _result.Address.ShouldEqual(_place.Address);

            It should_create_a_venue_dto_for_each_venue_at_the_place = () => _result.Venues.ShouldContainOnlyOrdered(_venueDto, _otherVenueDto);

            Establish context = () =>
            {
                _place = NewInstanceOf<IPlace>();
                _place.Stub(p => p.Longitude).Return(0.0003);
                _place.Stub(p => p.Latitude).Return(1.222);
                _place.Stub(p => p.PlaceId).Return("fdsa56");
                _place.Stub(p => p.Name).Return("Test name");
                _place.Stub(p => p.Address).Return("Test address");

                var venue = NewInstanceOf<Venue>();
                var otherVenue = NewInstanceOf<Venue>();
                _place.Stub(p => p.Venues).Return(new[] {venue, otherVenue});

                _venueDto = NewInstanceOf<VenueDto>();
                Injected<IVenueDtoFactory>().Stub(f => f.Create(venue)).Return(_venueDto);

                _otherVenueDto = NewInstanceOf<VenueDto>();
                Injected<IVenueDtoFactory>().Stub(f => f.Create(otherVenue)).Return(_otherVenueDto);
            };

            static IPlace _place;
            static PlaceDto _result;
            static VenueDto _otherVenueDto;
            static VenueDto _venueDto;
        }
    }
}