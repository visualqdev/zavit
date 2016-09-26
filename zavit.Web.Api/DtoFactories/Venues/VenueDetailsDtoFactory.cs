using System.Linq;
using zavit.Domain.Venues;
using zavit.Web.Api.Dtos.Venues;

namespace zavit.Web.Api.DtoFactories.Venues
{
    public class VenueDetailsDtoFactory : IVenueDetailsDtoFactory
    {
        readonly IVenueActivityDtoFactory _venueActivityDtoFactory;

        public VenueDetailsDtoFactory(IVenueActivityDtoFactory venueActivityDtoFactory)
        {
            _venueActivityDtoFactory = venueActivityDtoFactory;
        }

        public VenueDetailsDto Create(Venue venue)
        {
            var venueActivity = new VenueDetailsDto
            {
                Name = venue.Name,
                Address = venue.Address,
                Id = venue.Id,
                Activities = venue.Activities.Select(a => _venueActivityDtoFactory.CreateItem(a)),
                Longitude = venue.Longitude,
                Latitude = venue.Latitude
            };

            return venueActivity;
        }
    }
}