using zavit.Domain.Places;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.DtoFactories.Places
{
    public class PlaceDtoFactory : IPlaceDtoFactory
    {
        public PlaceDto CreateItem(IPlace place)
        {
            return new PlaceDto
            {
                Longitude = place.Longitude,
                Latitude = place.Latitude,
                PlaceId = place.PlaceId,
                Address = place.Address,
                Name = place.Name
            };
        }
    }
}