using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.Suggestions;

namespace zavit.Domain.Places.Tests.Suggestions 
{
    [Subject("PublicPlaceSuggestion")]
    public class PublicPlaceSuggestionTests : TestOf<PublicPlaceSuggestion>
    {
        class When_getting_longitude
        {
            It should_return_longitude_of_a_public_place = () => Subject.Longitude.ShouldEqual(_publicPlace.Longitude);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.Longitude = 0.01;
                Subject.PublicPlace = _publicPlace;
            };

            static PublicPlace _publicPlace;
        }

        class When_getting_latitude
        {
            It should_return_latitude_of_a_public_place = () => Subject.Latitude.ShouldEqual(_publicPlace.Latitude);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.Latitude = -0.02;
                Subject.PublicPlace = _publicPlace;
            };

            static PublicPlace _publicPlace;
        }

        class When_getting_place_id
        {
            It should_return_place_id_of_a_public_place = () => Subject.PlaceId.ShouldEqual(_publicPlace.PlaceId);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.PlaceId = "Place ID";
                Subject.PublicPlace = _publicPlace;
            };

            static PublicPlace _publicPlace;
        }

        class When_getting_name
        {
            It should_return_name_of_a_public_place = () => Subject.Name.ShouldEqual(_publicPlace.Name);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.Name = "Place name";
                Subject.PublicPlace = _publicPlace;
            };

            static PublicPlace _publicPlace;
        }

        class When_getting_address
        {
            It should_return_address_of_a_public_place = () => Subject.Address.ShouldEqual(_publicPlace.Address);

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.Address = "Place address";
                Subject.PublicPlace = _publicPlace;
            };

            static PublicPlace _publicPlace;
        }

        class When_getting_venues
        {
            It should_always_return_an_empty_collection = () => Subject.Venues.ShouldBeEmpty();
        }
    }
}

