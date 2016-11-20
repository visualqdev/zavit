using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Infrastructure.Places.PublicPlacesApis.Search;

namespace zavit.Infrastructure.Places.Tests.PublicPlacesApis.Search 
{
    [Subject("PlaceSearchTransformer")]
    public class PublicPlaceTransformerTests : TestOf<PlaceSearchTransformer>
    {
        class When_transforming_a_google_place_to_a_public_place
        {
            Because of = () => _result = Subject.Transform(_googlePlace);

            It should_set_the_place_id_to_be_the_id_of_a_google_place = () => _result.PlaceId.ShouldEqual(_googlePlace.place_id);

            It should_set_the_latitude_to_be_the_lat_of_a_google_place = () => _result.Latitude.ShouldEqual(_googlePlace.geometry.location.lat);

            It should_set_the_longitude_to_be_the_lng_of_a_google_place = () => _result.Longitude.ShouldEqual(_googlePlace.geometry.location.lng);

            It should_set_the_address_to_be_the_vicinity_of_a_google_place = () => _result.Address.ShouldEqual(_googlePlace.vicinity);

            It should_set_the_name_to_be_the_name_of_a_google_place = () => _result.Name.ShouldEqual(_googlePlace.name);

            Establish context = () =>
            {
                _googlePlace = NewInstanceOf<GooglePlaceSearch>();
                _googlePlace.place_id = "test_place_id";
                _googlePlace.name = "Name of place";
                _googlePlace.vicinity = "Vicinity";
                _googlePlace.geometry = new GooglePlaceSearchGeometry
                {
                    location = new GooglePlaceSearchLocation
                    {
                        lat = 0.01,
                        lng = -0.02
                    }
                };
            };

            static GooglePlaceSearch _googlePlace;
            static PublicPlace _result;
        }
    }
}

