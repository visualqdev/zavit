using zavit.Domain.Venues;
using zavit.Web.Api.Dtos.VenueMemberships;

namespace zavit.Web.Api.DtoFactories.VenueMemberships
{
    public class MembershipVenueDtoFactory : IMembershipVenueDtoFactory
    {
        public MembershipVenueDto Create(Venue venue)
        {
            var venueActivity = new MembershipVenueDto
            {
                Name = venue.Name,
                Address = venue.Address,
                Id = venue.Id,
                Longitude = venue.Longitude,
                Latitude = venue.Latitude
            };

            return venueActivity;
        }
    }
}