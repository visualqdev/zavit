using System.Linq;
using zavit.Domain.Places;
using zavit.Web.Api.DtoFactories.Venues;
using zavit.Web.Api.Dtos.Places;

namespace zavit.Web.Api.DtoFactories.Places
{
    public class PlaceDtoFactory : IPlaceDtoFactory
    {
        readonly IVenueDtoFactory _venueDtoFactory;

        public PlaceDtoFactory(IVenueDtoFactory venueDtoFactory)
        {
            _venueDtoFactory = venueDtoFactory;
        }

        public PlaceDto CreateItem(IPlace place)
        {
            return new PlaceDto
            {
                Longitude = place.Longitude,
                Latitude = place.Latitude,
                PlaceId = place.PlaceId,
                Address = place.Address,
                Name = place.Name,
                Venues = place.Venues.Select(v => _venueDtoFactory.Create(v))
            };
        }
    }
}