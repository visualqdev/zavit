using zavit.Domain.Places.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis
{
    public class PublicPlaceTransformer : IPublicPlaceTransformer
    {
        public PublicPlace Transform(GooglePlace googlePlace)
        {
            var publicPlace = new PublicPlace
            {
                PlaceId = googlePlace.place_id,
                Latitude = googlePlace.geometry.location.lat,
                Longitude = googlePlace.geometry.location.lng
            };

            return publicPlace;
        }
    }
}