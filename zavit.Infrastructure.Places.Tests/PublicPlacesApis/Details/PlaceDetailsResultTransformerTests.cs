using Machine.Specifications;
using Rhino.Mspec.Contrib;
using zavit.Domain.Venues.PublicPlaces;
using zavit.Infrastructure.Places.PublicPlacesApis.Details;

namespace zavit.Infrastructure.Places.Tests.PublicPlacesApis.Details 
{
    [Subject("PlaceDetailsResultTransformer")]
    public class PlaceDetailsResultTransformerTests : TestOf<PlaceDetailsResultTransformer>
    {
        class When_transforming_google_place_details_to_a_public_place
        {
            Because of = () => _result = Subject.Transform(_googlePlaceDetailsResults);

            It should_set_the_place_id_to_be_the_id_of_a_google_place = () => _result.PlaceId.ShouldEqual(_googlePlaceDetails.place_id);

            It should_set_the_latitude_to_be_the_lat_of_a_google_place = () => _result.Latitude.ShouldEqual(_googlePlaceDetails.geometry.location.lat);

            It should_set_the_longitude_to_be_the_lng_of_a_google_place = () => _result.Longitude.ShouldEqual(_googlePlaceDetails.geometry.location.lng);

            It should_set_the_address_to_be_the_vicinity_of_a_google_place = () => _result.Address.ShouldEqual(_googlePlaceDetails.vicinity);

            It should_set_the_name_to_be_the_name_of_a_google_place = () => _result.Name.ShouldEqual(_googlePlaceDetails.name);

            Establish context = () =>
            {
                _googlePlaceDetails = new GooglePlaceDetails
                {
                    place_id = "test_place_id",
                    name = "Name of place",
                    vicinity = "Vicinity",
                    geometry = new GooglePlaceDetailsGeometry
                    {
                        location = new GooglePlaceDetailsLocation
                        {
                            lat = 0.01,
                            lng = -0.02
                        }
                    }
                };
                _googlePlaceDetailsResults = NewInstanceOf<GooglePlaceDetailsResult>();
                _googlePlaceDetailsResults.result = _googlePlaceDetails;
            };

            static GooglePlaceDetailsResult _googlePlaceDetailsResults;
            static GooglePlaceDetails _googlePlaceDetails;
            static PublicPlace _result;
        }
    }
}

