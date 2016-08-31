using System.Threading.Tasks;
using Machine.Specifications;
using Rhino.Mocks;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Domain.Places.VenuePlaces;

namespace zavit.Domain.Places.Tests.VenuePlaces 
{
    [Subject("VenuePlaceCreator")]
    public class VenuePlaceCreatorTests : TestOf<VenuePlaceCreator>
    {
        class When_creating_a_venue_place
        {
            Because of = () => _result = Subject.Create(PlaceId).Result;

            It should_set_the_place_id_to_be_the_place_id_of_the_public_place = () => _result.PlaceId.ShouldEqual(_publicPlace.PlaceId);

            It should_set_the_address_to_be_the_address_of_the_public_place = () => _result.Address.ShouldEqual(_publicPlace.Address);

            It should_set_the_name_to_be_the_name_of_the_public_place = () => _result.Name.ShouldEqual(_publicPlace.Name);

            It should_set_the_latitude_to_be_the_latitude_of_the_public_place = () => _result.Latitude.ShouldEqual(_publicPlace.Latitude);

            It should_set_the_longitude_to_be_the_longitude_of_the_public_place = () => _result.Longitude.ShouldEqual(_publicPlace.Longitude);

            It should_set_the_venues_collection_to_be_an_empty_collection = () => _result.Venues.ShouldBeEmpty();

            Establish context = () =>
            {
                _publicPlace = NewInstanceOf<PublicPlace>();
                _publicPlace.PlaceId = "Place ID";
                _publicPlace.Address = "Place address";
                _publicPlace.Name = "Place name";
                _publicPlace.Latitude = 0.1;
                _publicPlace.Longitude = -0.2;

                Injected<IPublicPlacesService>().Stub(s => s.GetPublicPlace(PlaceId)).Return(Task.FromResult(_publicPlace));
            };

            static VenuePlace _result;
            static PublicPlace _publicPlace;
            const string PlaceId = "Place ID";
        }
    }
}

