using zavit.Domain.Venues.PublicPlaces;

namespace zavit.Infrastructure.Places.PublicPlacesApis.Search
{
    public class PlaceSearchTransformer : IPlaceSearchTransformer
    {
        public PublicPlace Transform(GooglePlaceSearch googlePlace)
        {
            var publicPlace = new PublicPlace
            {
                PlaceId = googlePlace.place_id,
                Latitude = googlePlace.geometry.location.lat,
                Longitude = googlePlace.geometry.location.lng,
                Name = googlePlace.name,
                Address = googlePlace.vicinity
            };

            return publicPlace;
        }
    }
}