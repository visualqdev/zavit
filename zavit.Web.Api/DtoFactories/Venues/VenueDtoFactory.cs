using System.Linq;
using zavit.Domain.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoFactories.Venues
{
    public class VenueDtoFactory : IVenueDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;

        public VenueDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
        }

        public VenueDto Create(IVenue venue)
        {
            var activityDtos = venue.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a));

            return new VenueDto
            {
                Id = venue.Id,
                Name = venue.Name,
                Latitude = venue.Latitude,
                Longitude = venue.Longitude,
                PublicPlaceId = venue.PublicPlaceId,
                Address = venue.Address,
                Activities = activityDtos
            };
        }
    }
}