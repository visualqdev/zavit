using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Places.PublicPlaces;
using zavit.Infrastructure.Places.PublicPlacesApis;

namespace zavit.Infrastructure.Places.Tests.PublicPlacesApis 
{
    [Subject("PublicPlaceTransformer")]
    public class PublicPlaceTransformerTests : TestOf<PublicPlaceTransformer>
    {
        class When_transforming_a_google_place_to_a_public_place
        {
            Because of = () => _result = Subject.Transform(_googlePlace);

            It should_set_the_place_id_to_be_the_id_of_a_google_place = () => _result.PlaceId.ShouldEqual(_googlePlace.place_id);

            It should_set_the_latitude_to_be_the_lat_of_a_google_place = () => _result.Latitude.ShouldEqual(_googlePlace.geometry.location.lat);

            It should_set_the_longitude_to_be_the_lng_of_a_google_place = () => _result.Longitude.ShouldEqual(_googlePlace.geometry.location.lng);

            Establish context = () =>
            {
                _googlePlace = NewInstanceOf<GooglePlace>();
                _googlePlace.place_id = "test_place_id";
                _googlePlace.geometry = new GoogleGeometry
                {
                    location = new GoogleLocation
                    {
                        lat = 0.01,
                        lng = -0.02
                    }
                };
            };

            static GooglePlace _googlePlace;
            static PublicPlace _result;
        }
    }
}

