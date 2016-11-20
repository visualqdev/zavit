using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Details
{
    public class PlaceDetailsResultTransformer : IPlaceDetailsResultTransformer
    {
        public PublicPlace Transform(GooglePlaceDetailsResult googlePlacesDetailsResult)
        {
            var googlePlaceDetails = googlePlacesDetailsResult.result;

            var publicPlace = new PublicPlace
            {
                PlaceId = googlePlaceDetails.place_id,
                Latitude = googlePlaceDetails.geometry.location.lat,
                Longitude = googlePlaceDetails.geometry.location.lng,
                Name = googlePlaceDetails.name,
                Address = googlePlaceDetails.vicinity
            };

            return publicPlace;
        }
    }
}